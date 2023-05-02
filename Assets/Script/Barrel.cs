using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    Animator animator;
    public GameObject barrel;

    private void OnTriggerEnter2D(Collider2D colision)
    {
        Instantiate(barrel, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
