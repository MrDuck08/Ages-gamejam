using System;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float staggerTime = 0.1f;
    [SerializeField] private LayerMask whatToHitLayerMask;
    [SerializeField] private bool doNotDestroySelfUponHit;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (( whatToHitLayerMask & (1 << other.gameObject.layer)) != 0)
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage, staggerTime);
            if(!doNotDestroySelfUponHit) Destroy(gameObject);
        }
    }
}
