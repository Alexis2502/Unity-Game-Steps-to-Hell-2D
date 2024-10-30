using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestScript : MonoBehaviour
{
    public Canvas textCanvas;
    public TextMeshProUGUI textPad;
    public TextMeshProUGUI startText;
    public List<string> dialogue;
    protected bool waitForNext;
    protected bool started;
    protected int index;
    private int idxChar;
    public float writingSpeed;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _sprite;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] protected QuestSystem questSystem;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        questSystem = FindAnyObjectByType<QuestSystem>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            startText.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                _spriteRenderer.sprite = _sprite[1];
                if (questSystem.checkIndex() == 0)
                {
                    questSystem.getCurrentQuest().goalCompleted();
                    startText.enabled = false;
                    textCanvas.enabled = true;
                    StartDialogue();
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            startText.enabled = false;
            _spriteRenderer.sprite = _sprite[0];
        }
    }

    private void Update()
    {
        if (!started)
            return;

        if (waitForNext && Input.GetMouseButtonDown((int)MouseButton.Left))
        {
            waitForNext = false;
            index++;

            //Check if we are in the scope fo dialogues List
            if (index < dialogue.Count)
            {
                //If so fetch the next dialogue
                GetDialogue(index);
            }
            else
            {
                EndDialogue();
                SceneManager.LoadScene("End");
            }
        }
    }
    public void StartDialogue()
    {
        if (started)
        {
            return;
        }
        started = true;
        GetDialogue(0);
    }

    public void GetDialogue(int i)
    {
        index = i;
        idxChar = 0;
        textPad.text = string.Empty;
        StartCoroutine(WriteDialogue());
    }

    IEnumerator WriteDialogue()
    {
            yield return new WaitForSeconds(writingSpeed);
            string currentDialogue = dialogue[index];
            textPad.text += currentDialogue[idxChar];
            idxChar++;

            if (idxChar < currentDialogue.Length)
            {
                yield return new WaitForSeconds(writingSpeed);
                StartCoroutine(WriteDialogue());
            }
            else
            {
                waitForNext = true;
            }
    }

    public void EndDialogue()
    {
        textCanvas.enabled = false;
        started = false;
        StopAllCoroutines();
        waitForNext = false;
        index = 0;
        idxChar = 0;
    }
}
