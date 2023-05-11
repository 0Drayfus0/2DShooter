using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    [Header("Param")]
    [SerializeField] Animator animator;
    [SerializeField] Player player;

    [Header ("Range")]
    [SerializeField]float attackRange = 5;
    [SerializeField]float escapeRange = 25;
    [SerializeField]float movementRange = 15;

    [Header("Statistics")]
    public Slider slider;
    public int health = 100;
    public int damage = 10;
   
    ZombieState activeState;
    ZombiesMovement movement;

    
    float distanceToPlayer;

    Vector3 startPosition;


    enum ZombieState
    {
        STAND,
        MOVE,
        RETURN,
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
        ChageState(ZombieState.STAND);
        slider.maxValue = health;
        slider.value = health;
        startPosition = transform.position;
    }
    public void updateHealth(int amount)
    {
        health += amount;
        slider.value = health;
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
        }

        activeState = newState;
    }
    private void DoStand()
    {
        if (distanceToPlayer < movementRange)
        {
            ChageState(ZombieState.MOVE);
            return;
        }

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
        movement.targetPosition = player.transform.position;
    }
    private void DoAttack()
    {
        if (distanceToPlayer > attackRange)
        {
            ChageState(ZombieState.MOVE);
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
