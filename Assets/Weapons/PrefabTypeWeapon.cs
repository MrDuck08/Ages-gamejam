using UnityEngine;
using UnityEngine.Events;

public class PrefabTypeWeapon : WeaponBaseClass
{
    [SerializeField] private bool amIRifle = false;
    [Header("Prefab settings")]
    [SerializeField] private float shootOutForce;
    [SerializeField] private GameObject bulletPrefab;
    
    protected override void ChargeUp()
    {
        // Inte long-term kodning
        if (amIRifle)
        {
            audioManager.RifleShootSound();
        }
        else
        {
            audioManager.PunchSoundPlay();
        }

        foreach (Transform shootPoint in shootPoints)
        {
            GameObject spawnObj = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            if (spawnObj != null)
            {
                Rigidbody2D bulletRigidBody = spawnObj.GetComponent<Rigidbody2D>();
                bulletRigidBody.AddForce(shootPoint.up * shootOutForce);   
            }
        }
    }

    public UnityEvent OnShoot;
    private void OnDrawGizmos()
    {
        foreach (Transform shootPoint in shootPoints)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(shootPoint.position, shootPoint.position + shootPoint.up * shootOutForce);
        }
    }
}