using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [Header ("Range")]
    [SerializeField]float attackRange = 5;
    [SerializeField]float escapeRange = 25;
    [SerializeField]float movementRange = 15;

    [Header("Statistics")]
    public int health = 100;
    public int damage = 10;

    ZombiesMovement movement;

    Animator animator;
    Player player;
    ZombieState activeState;
    float distanceToPlayer;


    enum ZombieState
    {
        STAND,
        MOVE,
        ATTACK
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<ZombiesMovement>();
    }
    private void Start()
    {
        player = FindObjectOfType<Player>();
        activeState = ZombieState.STAND;
    }
    public void updateHealth(int amount)
    {
        health += amount;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        switch (activeState)
        {
            case ZombieState.STAND:
                DoStand(distanceToPlayer);

                break;

            case ZombieState.MOVE:
                DoMove(distanceToPlayer);

                break;
                
            case ZombieState.ATTACK:
                DoAttack(distanceToPlayer);

                break;     
        }
    }
    private void DoStand(float distance)
    {
        if (distance < movementRange)
        {
            activeState = ZombieState.MOVE;
            return;
        }
        movement.enabled = false;
        animator.SetFloat("Speed", 0);
    }
    private void DoMove(float distance)
    {
        if (distance < attackRange)
        {
            activeState = ZombieState.ATTACK;
            return;
        }
        else if (distance > escapeRange)
        {
            activeState = ZombieState.STAND;
            return;
        }
        movement.enabled = true;
        animator.SetFloat("Speed", 1);
    }
    private void DoAttack(float distance)
    {
        if (distance > attackRange)
        {
            activeState = ZombieState.MOVE;
            return;
        }
        movement.enabled = false;
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
