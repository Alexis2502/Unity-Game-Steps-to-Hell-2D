using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Script : EnemyBase
{
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>().transform.position;
        if (Vector2.Distance(_player, _rb.transform.position) < 70)
        {
            _moveDirection = new Vector2(_player.x - transform.position.x, _player.y - transform.position.y).normalized;
            if (Vector2.Distance(_player, _rb.transform.position) < 10)
            {
                Attack();
            }
        }
        else
        {
            if (Time.time - latestDirectionChangeTime > directionChangeTime)
            {
                latestDirectionChangeTime = Time.time;
                calcuateNewMovementVector();
            }

            transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
            transform.position.y + (movementPerSecond.y * Time.deltaTime));
        }
        _animator.SetFloat("moveX", _moveDirection.x);
        _animator.SetFloat("moveY", _moveDirection.y);
        _animator.SetBool("isMoving", isMoving);
    }
    private void FixedUpdate()
    {
        if (_rb != null)
        {
            if ((_moveDirection.x > 0 || _moveDirection.y > 0) || (_moveDirection.x < 0 || _moveDirection.y < 0))
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
            _rb.transform.position = (Vector2)_rb.transform.position + _moveDirection * _speed;
        }
    }
    private void Attack()
    {
        if (_moveDirection.x > 0)
        {
            _animator.Play("enemy_attack_right");
        }
        else if (_moveDirection.x < 0)
        {
            _animator.Play("enemy_attack");
        }
        return;
    }
}
