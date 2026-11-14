using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : Health
{
    [Header("Player")]
    [SerializeField] private float invicibilityTime = 1f;
    private bool invincible;

    [SerializeField] private LayerMask layersWeCanTakeDamageFrom;

    [Header("Refrences for damage dealing")] 
    [SerializeField] private PlayerWeaponHandler weaponHandler;
    [SerializeField] private PlayerMovement playerMove;
    private BaseHull currentHull;
    
    public override void TakeDamage(float damage, float staggerTime)
    {
        if (!invincible)
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
            StartCoroutine(InvicibilityRoutine());
        }
    }
    
    private IEnumerator InvicibilityRoutine()
    {
        invincible = true;


        yield return new WaitForSeconds(invicibilityTime);


        invincible = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Blir träffad av en fiende
        if (( layersWeCanTakeDamageFrom & (1 << collision.gameObject.layer)) != 0)
        {
            Debug.Log("Take Damage");
            TakeDamage(5f, 1337);
        }
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
