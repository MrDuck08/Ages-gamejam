using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [Header("Enemy")]
    [SerializeField] private List<GameObject> commonWeaponsList = new List<GameObject>();
    [SerializeField] private List<GameObject> rareWeaponsList = new List<GameObject>();

    [SerializeField] private List<GameObject> hullDropsList = new List<GameObject>();
    [SerializeField] private List<GameObject> legsDropList = new List<GameObject>();

    [SerializeField] private int chanceForDrop = 20;

    private EnemyMove enemyMove;
    TestSpawner spawner;
    
    private void Start()
    {
        enemyMove = GetComponent<EnemyMove>();

        spawner = FindAnyObjectByType<TestSpawner>();
    }
    
    public override void TakeDamage(float damage, float staggerTime)
    {
        StartCoroutine(enemyMove.StaggerRoutine(staggerTime));
        base.TakeDamage(damage, staggerTime);
    }

    public override void DestroySelf()
    {
        int dropChanse =  Random.Range(0, chanceForDrop + spawner.decreaseChanceForSpawn);

        // Den ska droppa
        if(dropChanse == 0)
        {
            int whatToDrop = Random.Range(0, 5);

            //Drop weapon
            if(whatToDrop <= 2)
            {
                int rareWeaponDropList = Random.Range(0, 3);

                int whatWeaponDrop = Random.Range(0, commonWeaponsList.Count);
                if (rareWeaponDropList == 0)
                {
                    whatWeaponDrop = Random.Range(0, rareWeaponsList.Count);
                    GameObject spawnedObj = Instantiate(rareWeaponsList[whatWeaponDrop]);
                    spawnedObj.transform.position = transform.position;
                }
                else
                {
                    GameObject spawnedObj = Instantiate(commonWeaponsList[whatWeaponDrop]);
                    spawnedObj.transform.position = transform.position;
                }

            }
            else
            {
                int hullDrop = Random.Range(0, 3);

                int whatWeaponDrop = Random.Range(0, legsDropList.Count);
                if (hullDrop == 0)
                {
                    whatWeaponDrop = Random.Range(0, hullDropsList.Count);
                    GameObject spawnedObj = Instantiate(hullDropsList[whatWeaponDrop]);
                    spawnedObj.transform.position = transform.position;
                }
                else
                {
                    GameObject spawnedObj = Instantiate(legsDropList[whatWeaponDrop]);
                    spawnedObj.transform.position = transform.position;
                }

            }

        }
        base.DestroySelf();
    }
}