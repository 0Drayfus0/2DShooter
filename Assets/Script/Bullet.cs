using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        rb.velocity = -transform.up * speed;
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
