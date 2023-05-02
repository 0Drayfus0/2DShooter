using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPref;
    public GameObject pistol;

    public int health = 100;

    float _fireRate = 0.5f;
    float _nextFire;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void updateHealth(int amount)
    {
        health += amount;
    }
    private void Update()
    {
        if (Input.GetButton("Fire1") && _nextFire <= 0)
        {
            animator.SetTrigger("Shoot");
            Instantiate(bulletPref, pistol.transform.position, transform.rotation);
            _nextFire = _fireRate;
        }

        if(_nextFire >= 0)
        {
            _nextFire -= Time.deltaTime;
        }
       
    }
}
