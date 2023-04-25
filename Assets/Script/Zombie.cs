using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField]float attackRange = 5;
    [SerializeField]float movementRange = 15;

    ZombiesMovement movement;

    Animator animator;
    Player player;
    ZombieState activeState;


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

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        switch (activeState)
        {
            case ZombieState.STAND:
                if(distance < movementRange)
                {
                    activeState = ZombieState.MOVE;
                    return;
                }
                movement.enabled = false;
                animator.SetFloat("Speed", 0);
                break;

            case ZombieState.MOVE:
                if(distance < attackRange)
                {
                    activeState = ZombieState.ATTACK;
                    return;
                }
                else if(distance > movementRange)
                {
                    activeState = ZombieState.STAND;
                    return;
                }
                movement.enabled = true;
                animator.SetFloat("Speed", 1);
                break;

            case ZombieState.ATTACK:
                if(distance > attackRange)
                {
                    activeState = ZombieState.MOVE;
                    return;
                }
                movement.enabled = false;
                animator.SetTrigger("Shoot");
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, movementRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
