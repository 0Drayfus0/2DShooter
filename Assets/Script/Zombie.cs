using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    public Action OnChangeHealth = delegate { };

    [Header("Param")]
    [SerializeField] Animator animator;
    [SerializeField] Player player;

    [Header ("Range")]
    [SerializeField]float attackRange = 5;
    [SerializeField]float escapeRange = 25;
    [SerializeField]float movementRange = 15;
    [SerializeField]public int viewAnge = 90;

    [Header("Statistics")]
    public int health = 100;
    public int damage = 10;
    public bool death;
   
    ZombieState activeState;
    ZombiesMovement movement;

    
    float distanceToPlayer;

    Vector3 startPosition;


    enum ZombieState
    {
        STAND,
        MOVE,
        RETURN,
        ATTACK,
        DEATH
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<ZombiesMovement>();
    }
    private void Start()
    {
        player = FindObjectOfType<Player>();
        ChageState(ZombieState.STAND);
        startPosition = transform.position;
    }
    public void updateHealth(int amount)
    {
        health += amount;
        OnChangeHealth();

        if(health <= 0)
        {
            death = true;
            //
        }
    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        switch (activeState)
        {
            case ZombieState.STAND:
                DoStand();

                break;
            case ZombieState.RETURN:
                DoReturn();
                break;

            case ZombieState.MOVE:
                DoMove();

                break;
                
            case ZombieState.ATTACK:
                DoAttack();
                break;

            case ZombieState.DEATH:
                DoDeath();
                break;

        }
    }
    private void ChageState(ZombieState newState)
    {
        switch (newState)
        {
            case ZombieState.STAND:
                movement.enabled = false;
                break;

            case ZombieState.RETURN:
                movement.enabled = true;
                movement.targetPosition = startPosition;
                break;

            case ZombieState.MOVE:
                movement.enabled = true;
                break;

            case ZombieState.ATTACK:
                movement.enabled = false;
                break;

            case ZombieState.DEATH:
                movement.enabled = false;
                break;
        }

        activeState = newState;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            updateHealth(-bullet.damage);
            Destroy(bullet.gameObject);
        }
    }
    private void DoStand()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float angle = Vector3.Angle(-transform.up, directionToPlayer);
        if(angle > viewAnge / 2)
        {
            return;
        }

        if (distanceToPlayer < movementRange)
        {
            ChageState(ZombieState.MOVE);
            return;
        }


        else if (death)
        {
            isDeath();
            return;
        }

    }

    private void isDeath()
    {
        ChageState(ZombieState.DEATH);

    }

    private void DoDeath()
    {
        animator.SetTrigger("Death");
    }
    private void DoReturn()
    {
        if (distanceToPlayer < movementRange)
        {
            ChageState(ZombieState.MOVE);
            return;
        }

        float distanceToStartPos = Vector3.Distance(startPosition, transform.position);
        if(distanceToStartPos < 0.05f)
        {
            ChageState(ZombieState.STAND);
            return;
        }
        else if (death)
        {
            isDeath();
            return;
        }
    }
   
    private void DoMove()
    {
        if (distanceToPlayer < attackRange)
        {
            ChageState(ZombieState.ATTACK);
            return;
        }
        else if (distanceToPlayer > escapeRange)
        {
            ChageState(ZombieState.RETURN);
            return;
        }
        else if (death)
        {
            isDeath();
            return;
        }
        movement.targetPosition = player.transform.position;
        
    }
    private void DoAttack()
    {
        if (distanceToPlayer > attackRange)
        {
            ChageState(ZombieState.MOVE);
            return;
        }
        else if (death)
        {
            isDeath();
            return;
        }
        animator.SetTrigger("Shoot");
        
    }

    public void DamageToPlayer()
    {
        if(distanceToPlayer > attackRange)
        {
            return;
        }
        player.updateHealth(-damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, movementRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, escapeRange);

    }
}
