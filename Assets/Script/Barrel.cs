using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private float explodeRadius = 12;
    [SerializeField] private int damage;
    public LayerMask layerMask;
    public GameObject barrel;

    private void OnTriggerEnter2D(Collider2D colision)
    {
        Collider2D[] coliders = Physics2D.OverlapCircleAll(transform.position, explodeRadius, layerMask);
        foreach (Collider2D collider in coliders)
        {
            if (collider.gameObject.CompareTag("Zombie"))
            {
                Zombie zombie = collider.GetComponent<Zombie>();
                zombie.updateHealth(-damage);
            }
            else if (collider.gameObject.CompareTag("Player"))
            {
                Player player = collider.GetComponent<Player>();
                player.updateHealth(-damage);
            }
        }
        Instantiate(barrel, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }
}
