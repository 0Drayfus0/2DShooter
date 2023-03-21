using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        Move();
        Rotate();
    }
    private void Move()
    {

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(inputX, inputY);
        animator.SetFloat("Speed", direction.magnitude);
        if(direction.magnitude > 1)
        {
            direction = direction.normalized;
        }
        rb.velocity = direction * speed;

        // Vector3 playerNewPosition = transform.position;
        //  playerNewPosition.x += speed * Time.deltaTime * inputX;
        // playerNewPosition.y += speed * Time.deltaTime * inputY;
        // transform.position = playerNewPosition;
    }
    private void Rotate()
    {
        Vector3 playerPosition = transform.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = playerPosition - mousePosition;

        transform.up = (Vector2)direction;
        
    }

}
