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
    EnemySpawner spawner;
    private AudioManager audioManager;
    
    private void Start()
    {
        enemyMove = GetComponent<EnemyMove>();

        spawner = FindAnyObjectByType<EnemySpawner>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }
    
    public override void TakeDamage(float damage, float staggerTime)
    {
        StartCoroutine(enemyMove.StaggerRoutine(staggerTime));
        base.TakeDamage(damage, staggerTime);
    }

    public override void DestroySelf()
    {
        audioManager.EnemyDieSoundPlay(transform.position);

        int dropChanse =  Random.Range(0, chanceForDrop + spawner.decreaseChanceForSpawn);

        // Den ska droppa
        if(dropChanse == 0)
        {
            int whatToDrop = Random.Range(0, 10);

            //Drop weapon
            if(whatToDrop <= 6)
            {
                int rareWeaponDropList = Random.Range(0, 3);

                int whatWeaponDrop = Random.Range(0, commonWeaponsList.Count);
                if (rareWeaponDropList == 0)
                {
                    whatWeaponDrop = Random.Range(0, rareWeaponsList.Count);
                    GameObject spawnedObj = Instantiate(rareWeaponsList[whatWeaponDrop]);
                    spawnedObj.transform.position = transform.position;
                    spawnedObj.transform.rotation = Quaternion.Euler(0,0,Random.Range(-180f, 180f));
                }
                else
                {
                    GameObject spawnedObj = Instantiate(commonWeaponsList[whatWeaponDrop]);
                    spawnedObj.transform.position = transform.position;
                    spawnedObj.transform.rotation = Quaternion.Euler(0,0,Random.Range(-180f, 180f));
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
                    spawnedObj.transform.rotation = Quaternion.Euler(0,0,Random.Range(-180f, 180f));
                }
                else
                {
                    GameObject spawnedObj = Instantiate(legsDropList[whatWeaponDrop]);
                    spawnedObj.transform.position = transform.position;
                    spawnedObj.transform.rotation = Quaternion.Euler(0,0,Random.Range(-180f, 180f));
                }

            }

        }
        base.DestroySelf();
    }
}