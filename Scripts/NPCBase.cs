using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class NPCBase : MonoBehaviour
{
    public Canvas textCanvas;
    public TextMeshProUGUI textPad;
    public TextMeshProUGUI startText;
    public List<string> dialogue;
    public List<string> dialogue2;
    protected bool waitForNext;
    protected bool started;
    protected int index;
    private int idxChar;
    public float writingSpeed;
    [SerializeField] protected QuestSystem questSystem;
    public bool talkTwo;

    private void Start()
    {
        questSystem = FindAnyObjectByType<QuestSystem>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            startText.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                startText.enabled = false;
                textCanvas.enabled = true;
                StartDialogue();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            startText.enabled = false;
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
        if (!talkTwo)
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
        } else
        {
            yield return new WaitForSeconds(writingSpeed);
            string currentDialogue = dialogue2[index];
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
    }

    public void EndDialogue()
    {
        textCanvas.enabled = false;
        started = false;
        StopAllCoroutines();
        waitForNext = false;
        index = 0;
        idxChar = 0;
        if (!talkTwo)
        {
            talkTwo = true;
        }
    }
}
