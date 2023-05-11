using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiesMovement : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody2D rb;
    Animator animator;
    public Vector3 targetPosition;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

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
        animator.SetFloat("Speed", direction.magnitude);
        rb.velocity = -direction * speed;
    }
    private void Rotate(Vector3 direction)
    {
        transform.up = (Vector2)direction;
    }
    private Vector3 GetDirection()
    {
        Vector3 zombiePosition = transform.position;
        Vector3 direction = zombiePosition - targetPosition;
        return direction;
    }

    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
    }
}
