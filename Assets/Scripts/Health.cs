using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;

    [Header("Enemy")]
    [SerializeField] private List<GameObject> weaponsObjList = new List<GameObject>();

    int chanceForDrop = 20;

    EnemyMove enemyMove;

    private void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
    }

    public void TakeDamage(float damage, float staggerTime)
    {
        health -= damage;
        if (health <= 0) {OnDeath.Invoke();}
        else
        {

            if(enemyMove != null)
            {
                StartCoroutine(enemyMove.StaggerRoutine(staggerTime));
            }
        }
    }

    public UnityEvent OnDeath;
    public void DestroySelf()
    {
        // Bara kollar så att det är en fiende som dör
        if (enemyMove != null)
        {
            int dropChanse =  Random.Range(0, chanceForDrop);

            // Den ska droppa
            if(dropChanse == 0)
            {

                int whatWeaponDrop = Random.Range(0, weaponsObjList.Count);

                GameObject spawnedObj = Instantiate(weaponsObjList[whatWeaponDrop]);
                spawnedObj.transform.position = transform.position;

            }
        }

        Destroy(gameObject);
    }
}
