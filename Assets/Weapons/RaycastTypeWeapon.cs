using System;
using UnityEngine;

public class RaycastTypeWeapon : WeaponBaseClass
{
    [Header("Raycast settings")]
    [SerializeField] private float shotDistance;
    [SerializeField] private float damage;
    [SerializeField] private float staggerTime = 1.5f;
    [SerializeField] private float chargeUpTime = 0f;
    float currentChargeTime = 0f;
    bool startedChargeUp = false;
    [SerializeField] private bool raycastAll = false;

    [SerializeField] private GameObject trailEffectObj;

    [Header("System stuff")]
    [SerializeField] private LayerMask layersToCheckFiringFor;


    public override void Update()
    {
        base.Update();

        if (startedChargeUp)
        {
            currentChargeTime -= Time.deltaTime;

            if(currentChargeTime < 0f)
            {
                startedChargeUp = false;
                ShotWeapon();
            }
        }
    }

    protected override void ChargeUp()
    {
        startedChargeUp = true;
        currentChargeTime = chargeUpTime;
    }

    void ShotWeapon()
    {
        if (raycastAll)
        {
            foreach (Transform shootPoint in shootPoints)
            {

                RaycastHit2D[] hit = Physics2D.RaycastAll(shootPoint.position, shootPoint.up, shotDistance, layersToCheckFiringFor);
                if (hit != null)
                {

                    //we hit something!
                    //Debug.Log(hit.Length + " How mnay enemies hit");
                    for (int i = 0; i < hit.Length; i++)
                    {

                        hit[i].collider.gameObject.GetComponent<Health>().TakeDamage(damage, staggerTime);

                    }
                }

                GameObject trail = Instantiate(trailEffectObj);
                trail.GetComponent<BulletTrailEffect>().MoveAndFadeTrail(shootPoint.position, shootPoint.position + shootPoint.up * shotDistance);

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

                GameObject trail = Instantiate(trailEffectObj);
                trail.GetComponent<BulletTrailEffect>().MoveAndFadeTrail(shootPoint.position, shootPoint.position + shootPoint.up * shotDistance);
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