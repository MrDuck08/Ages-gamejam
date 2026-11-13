using UnityEngine;

public class PrefabTypeWeapon : WeaponBaseClass
{
    [Header("Prefab settings")]
    [SerializeField] private float shootOutForce;
    [SerializeField] private GameObject bulletPrefab;
    
    protected override void ShootWeapon()
    {
        foreach (Transform shootPoint in shootPoints)
        {
            Rigidbody2D bulletRigidBody = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody2D>();
            bulletRigidBody.AddForce(shootPoint.up * shootOutForce);
        }
    }
    
    private void OnDrawGizmos()
    {
        foreach (Transform shootPoint in shootPoints)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(shootPoint.position, shootPoint.position + shootPoint.up * shootOutForce);
        }
    }
}