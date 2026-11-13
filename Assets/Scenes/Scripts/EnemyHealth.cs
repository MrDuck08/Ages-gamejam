using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health = 100;

    [SerializeField] int chanceForDrop = 10;

    EnemyMove enemyMove;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage, float staggerTime)
    {

        health -= damage;


        if(health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(enemyMove.StaggerRoutine(staggerTime));
        }

    }



    void Die()
    {

        int dropOrNotInt = Random.Range(0, chanceForDrop);

        // Drop weapon
        if(dropOrNotInt == 1)
        {

            int randomDrop = Random.Range(0, 2);

            switch (randomDrop)
            {
                // Weapon drop
                case 0:


                    break;


                // Hull drop
                case 1:


                    break;


                // Leg Drop
                case 2:


                    break;

            }

        }

    }
}
