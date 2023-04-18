using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiesMovement : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody2D rb;
    Animator animator;
    Player player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        GetDirection();
        Move(GetDirection());
        Rotate(GetDirection());

    }
    private void Move(Vector3 direction)
    {
        if (direction.magnitude > 1)
        {
            direction = direction.normalized;
        }
        rb.velocity = -direction * speed;
    }
    private void Rotate(Vector3 direction)
    {
        transform.up = (Vector2)direction;
    }
    private Vector3 GetDirection()
    {
        Vector3 zombiePosition = transform.position;
        Vector3 playerPosition = player.transform.position;
        Vector3 direction = zombiePosition - playerPosition;
        return direction;
    }
}
