using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chest1Script : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _sprite;
    [SerializeField] private PlayerController _playerController;
    public TextMeshProUGUI startText;
    [SerializeField] protected QuestSystem questSystem;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            startText.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                startText.enabled = false;
                _spriteRenderer.sprite = _sprite[1];
                if (questSystem.checkIndex() == 2)
                {
                    questSystem.getCurrentQuest().goalCompleted();
                    questSystem.StartQuest();
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
}
