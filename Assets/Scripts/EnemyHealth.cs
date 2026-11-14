using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [Header("Enemy")]
    [SerializeField] private List<GameObject> commonWeaponsList = new List<GameObject>();
    [SerializeField] private List<GameObject> rareWeaponsList = new List<GameObject>();

    [SerializeField] private int chanceForDrop = 20;

    private EnemyMove enemyMove;
    
    private void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
    }
    
    public override void TakeDamage(float damage, float staggerTime)
    {
        StartCoroutine(enemyMove.StaggerRoutine(staggerTime));
        base.TakeDamage(damage, staggerTime);
    }

    public override void DestroySelf()
    {
        int dropChanse =  Random.Range(0, chanceForDrop);

        // Den ska droppa
        if(dropChanse == 0)
        {
            int rareWeaponDropList = Random.Range(0, 3);

            int whatWeaponDrop = Random.Range(0, commonWeaponsList.Count);
            if (rareWeaponDropList == 0)
            {
                whatWeaponDrop = Random.Range(0, rareWeaponsList.Count);
            }


            GameObject spawnedObj = Instantiate(commonWeaponsList[whatWeaponDrop]);
            spawnedObj.transform.position = transform.position;

        }
        base.DestroySelf();
    }
}