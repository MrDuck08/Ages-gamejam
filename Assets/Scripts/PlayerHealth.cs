using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : Health
{
    [Header("Player")]
    [SerializeField] private float invicibilityTime = 1f;
    private bool invincible;

    [Header("Refrences for damage dealing")] 
    [SerializeField] private PlayerWeaponHandler weaponHandler;
    [SerializeField] private PlayerMovement playerMove;
    //here we also need a refrence to the hull.
    
    public override void TakeDamage(float damage, float staggerTime)
    {
        if (!invincible)
        {
            float damageCalculating = damage;
            
            //hull will mitigate damage only for health taking

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
        // Detta är spelaren som får en collision
        Debug.Log(collision.gameObject.layer + " What layer");
        
        // Blir träffad av en fiende
        if(collision.gameObject.layer == 6)
        {
            Debug.Log("Take Damage");
            TakeDamage(5f, 1337);
        }
    }
}
