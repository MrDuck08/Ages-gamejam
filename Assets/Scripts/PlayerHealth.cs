using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : Health
{
    [Header("Player")]
    [SerializeField] private float invicibilityTime = 1f;
    private bool invincible;
    
    public override void TakeDamage(float damage, float staggerTime)
    {
        if (!invincible)
        {
            base.TakeDamage(damage, staggerTime);
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
