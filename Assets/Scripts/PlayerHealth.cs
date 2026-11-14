using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : Health
{
    public static PlayerHealth Singleton;
    
    [Header("Player")]
    public float Health => health;
    
    [SerializeField] private LayerMask layersWeCanTakeDamageFrom;

    [Header("Refrences for damage dealing")] 
    [SerializeField] private PlayerWeaponHandler weaponHandler;
    [SerializeField] private PlayerMovement playerMove;
    private BaseHull currentHull;
    public BaseHull Hull => currentHull;

    public void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }

        Singleton = this;
    }

    public override void TakeDamage(float damage, float staggerTime)
    {
        float damageCalculating = damage;
            
        //hull will mitigate damage only for health taking
        if (currentHull != null)
        {
            damageCalculating = currentHull.CalculateHowMuchLeftOver(damage);
        }

        playerMove.LegsTakeDurabilityDamage(damage / 3f);
        weaponHandler.TakeDurabilityDamage(damage / 3f);
            
        base.TakeDamage(damageCalculating, staggerTime);
    }

    public void SwapHull(BaseHull newHull)
    {
        if (currentHull != null)
        {
            currentHull.DropSelf();
        }

        currentHull = newHull;
        currentHull.BePickedUp(transform);
    }
}
