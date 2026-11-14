using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;

    [Header("Player")]
    [SerializeField] private float invicibilityTime = 1f;
    bool invincible = false;

    [Header("Enemy")]
    [SerializeField] private List<GameObject> commonWeaponsList = new List<GameObject>();
    [SerializeField] private List<GameObject> rareWeaponsList = new List<GameObject>();

    int chanceForDrop = 20;

    EnemyMove enemyMove;

    private void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
    }

    public void TakeDamage(float damage, float staggerTime)
    {
        // Detta är för spelare, på fienden är den alltid false
        if (!invincible)
        {
            health -= damage;
        }

        // Spelare
        if(enemyMove == null)
        {
            StartCoroutine(InvicibilityRoutine());
        }

        if (health <= 0) {OnDeath.Invoke();}
        else
        {

            if(enemyMove != null)
            {
                // Fiende
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
                int rareWeaponDropList = Random.Range(0, 3);

                int whatWeaponDrop = Random.Range(0, commonWeaponsList.Count);
                if (rareWeaponDropList == 0)
                {
                    whatWeaponDrop = Random.Range(0, rareWeaponsList.Count);
                }


                GameObject spawnedObj = Instantiate(commonWeaponsList[whatWeaponDrop]);
                spawnedObj.transform.position = transform.position;

            }
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        // Detta är spelaren som får en collision
        if(enemyMove == null)
        {
            // Blir träffad av en fiende
            if(collision.gameObject.layer == 6)
            {
                Debug.Log("Take Damage");
                TakeDamage(5f, 1337);

            }

        }

    }

    IEnumerator InvicibilityRoutine()
    {
        invincible = true;


        yield return new WaitForSeconds(invicibilityTime);


        invincible = false;
    }
}
