using System;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private LayerMask whatToHitLayerMask;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (( whatToHitLayerMask & (1 << other.gameObject.layer)) != 0)
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
