using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] protected float health;
    public UnityEvent OnDeath;

    public virtual void TakeDamage(float damage, float staggerTime)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDeath.Invoke();
        }
    }
    
    public virtual void DestroySelf()
    {
        Destroy(gameObject);
    }
}
