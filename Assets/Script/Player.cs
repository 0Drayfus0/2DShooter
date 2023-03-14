using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPref;
    public GameObject pistol;

    float _fireRate = 0.5f;
    float _nextFire;

    private void Update()
    {
        if (Input.GetButton("Fire1") && _nextFire <= 0)
        {
            Instantiate(bulletPref, pistol.transform.position, transform.rotation);
            _nextFire = _fireRate;
        }

        if(_nextFire >= 0)
        {
            _nextFire -= Time.deltaTime;
        }
       
    }
}
