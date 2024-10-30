using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, LifeCount
{
    [SerializeField] private float _moveSpeed;
    private Rigidbody2D _rb;
    private Vector2 _moveDirection;
    private bool isMoving;
    public UnityEngine.UI.Image[] lives;
    public int livesRemaining;
    public bool hasWeapon;

    private Vector2 _shootDirection;
    [SerializeField] private Rigidbody2D _arrowPrefab;
    [SerializeField] private float _arrowSpeed = 12.5f;

    private Animator _animator;
    [SerializeField] protected QuestSystem questSystem;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        questSystem = FindAnyObjectByType<QuestSystem>();
        questSystem.StartQuest();

    }
    private void Update()
    {
        _moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _animator.SetFloat("moveX", _moveDirection.x);
        _animator.SetFloat("moveY", _moveDirection.y);
        _animator.SetBool("isMoving", isMoving);
    }

    private void FixedUpdate()
    {
        if (_rb != null)
        {
            if((_moveDirection.x > 0 ||  _moveDirection.y > 0) || (_moveDirection.x < 0 || _moveDirection.y < 0))
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (hasWeapon)
                    {
                        AttackWeapon();
                    }
                    else
                    {
                        Attack();
                    }
                }
                isMoving = true;
            } else
            {
                isMoving = false;
            }
            _rb.transform.position = (Vector2)_rb.transform.position + _moveDirection * _moveSpeed;
        }
    }

    public void LoseLife()
    {
        //If no lives remaining do nothing
        if (livesRemaining == 0)
            return;
        //Decrease the value of livesRemaining
        livesRemaining--;
        //Hide one of the life images
        lives[livesRemaining].enabled = false;

        //If we run out of lives we lose the game
        if (livesRemaining == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void GainLife()
    {
        for (int i = livesRemaining - 1; i < 5; i++)
        {
            lives[i].enabled = true;
            livesRemaining = 5;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            LoseLife();
        }
    }

    private void Attack()
    {
        if (_moveDirection.x > 0)
        {
            _animator.Play("Player_attack_side");
        } else if (_moveDirection.y > 0)
        {
            _animator.Play("Player_attack_back");
        } else if(_moveDirection.x < 0)
        {
            _animator.Play("Player_attack_side_left");
        } else if(_moveDirection.y < 0)
        {
            _animator.Play("Player_attack");
        }
        return;
    }
    private void AttackWeapon()
    {
        _shootDirection = _moveDirection.normalized;
        if (_moveDirection.x > 0)
        {
            _animator.Play("Player_attack_weapon_side");
            Rigidbody2D arrow = Instantiate(_arrowPrefab, transform.position, Quaternion.identity);
            arrow.velocity = new Vector2(_arrowSpeed * _shootDirection.x, 0);
        }
        else if (_moveDirection.y > 0)
        {
            _animator.Play("Player_attack_weapon_back");
            Rigidbody2D arrow = Instantiate(_arrowPrefab, transform.position, Quaternion.identity);
            arrow.velocity = new Vector2(0, _arrowSpeed * _shootDirection.y);
        }
        else if (_moveDirection.x < 0)
        {
            _animator.Play("Player_attack_weapon_side_left");
            Rigidbody2D arrow = Instantiate(_arrowPrefab, transform.position, Quaternion.identity);
            arrow.velocity = new Vector2(_arrowSpeed * _shootDirection.x, 0);
        }
        else if (_moveDirection.y < 0)
        {
            _animator.Play("Player_attack_weapon");
            Rigidbody2D arrow = Instantiate(_arrowPrefab, transform.position, Quaternion.identity);
            arrow.velocity = new Vector2(0,_arrowSpeed * _shootDirection.y);
        }
        return;
    }
}
