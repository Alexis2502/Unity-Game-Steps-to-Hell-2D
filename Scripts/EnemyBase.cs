using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, LifeCount
{
    protected Rigidbody2D _rb;
    [SerializeField] protected float _speed = 1f;
    [SerializeField] protected Vector2 _player;
    protected Vector2 _moveDirection;
    protected bool isMoving;
    protected Animator _animator;
    protected float latestDirectionChangeTime;
    protected readonly float directionChangeTime = 3f;
    protected Vector2 movementPerSecond;
    public int livesRemaining;
    [SerializeField] private Animator _animator_player;
    [SerializeField] private PlayerController playerController;

    public void calcuateNewMovementVector()
    {
        _moveDirection = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f)).normalized;
        movementPerSecond = _moveDirection * _speed;
    }

    public void LoseLife()
    {
        //If no lives remaining do nothing
        if (livesRemaining == 0)
            return;
        //Decrease the value of livesRemaining
        livesRemaining--;

        //If we run out of lives we lose the game
        if (livesRemaining == 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && (_animator_player.GetCurrentAnimatorStateInfo(0).IsName("Player_attack") 
            || (_animator_player.GetCurrentAnimatorStateInfo(0).IsName("Player_attack_back")) 
            || (_animator_player.GetCurrentAnimatorStateInfo(0).IsName("Player_attack_side")) 
            || (_animator_player.GetCurrentAnimatorStateInfo(0).IsName("Player_attack_side_left"))))
        {
            LoseLife();
        } else if (collision.gameObject.tag == "Arrow")
        {
            LoseLife();
        }
    }

    public void Die()
    {
        _animator.Play("enemy_death");
        playerController.GainLife();
        Destroy(gameObject,1);
    }
}
