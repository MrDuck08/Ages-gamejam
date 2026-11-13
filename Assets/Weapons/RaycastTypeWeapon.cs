using System;
using UnityEngine;

public class RaycastTypeWeapon : WeaponBaseClass
{
    [Header("Raycast settings")]
    [SerializeField] private float shotDistance;
    
    [Header("System stuff")]
    [SerializeField] private LayerMask layersToCheckFiringFor;
    
    protected override void ShootWeapon()
    {
        foreach (Transform shootPoint in shootPoints)
        {
            RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, shootPoint.up, shotDistance, layersToCheckFiringFor);
            if (hit.collider != null)
            {
                //we hit something!
                Debug.Log("We hit the following object! " + hit.collider.gameObject);
                //hit.collider.gameObject.GetComponent<Health>().TakeDamage(pelletDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        foreach (Transform shootPoint in shootPoints)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(shootPoint.position, shootPoint.position + shootPoint.up * shotDistance);
        }
    }
}