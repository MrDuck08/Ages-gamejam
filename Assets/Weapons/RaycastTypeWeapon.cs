using System;
using UnityEngine;

public class RaycastTypeWeapon : WeaponBaseClass
{
    [Header("Raycast settings")]
    [SerializeField] private float shotDistance;
    [SerializeField] private float damage;
    [SerializeField] private float staggerTime = 1.5f;
    [SerializeField] private bool raycastAll = false;

    [Header("System stuff")]
    [SerializeField] private LayerMask layersToCheckFiringFor;
    
    protected override void ShootWeapon()
    {
        if (raycastAll)
        {
            foreach (Transform shootPoint in shootPoints)
            {

                RaycastHit2D[] hit = Physics2D.RaycastAll(shootPoint.position, shootPoint.up, shotDistance, layersToCheckFiringFor);
                if (hit != null)
                {
                    //we hit something!

                    for (int i = 0; i < hit.Length; i++)
                    {

                        hit[i].collider.gameObject.GetComponent<Health>().TakeDamage(damage, staggerTime);

                    }
                }
            }
        }
        else
        {
            foreach (Transform shootPoint in shootPoints)
            {

                RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, shootPoint.up, shotDistance, layersToCheckFiringFor);
                if (hit.collider != null)
                {
                    //we hit something!
                    hit.collider.gameObject.GetComponent<Health>().TakeDamage(damage, staggerTime);
                }
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