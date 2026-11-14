using System.Collections;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemyObj;

    [SerializeField] Vector2 minSpawnPos = Vector2.zero;
    [SerializeField] Vector2 maxSpawnPos = Vector2.zero;


    float enemySpawnInterval = 0.4f;
    int maxEnemySpawn = 10;

    float difficultyIncreaseInterval = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        //StartCoroutine(IncreaseDifficultyRoutine());
    }


    IEnumerator SpawnEnemyRoutine()
    {

        yield return new WaitForSeconds(enemySpawnInterval);



        int whereToSpawn = Random.Range(0, 4);


        float xSpawnPos = 0;
        float ySpawnPos = 0;

        switch (whereToSpawn)
        {
            //Above
            case 0:

                xSpawnPos = Random.Range(-maxSpawnPos.x, maxSpawnPos.x);
                ySpawnPos = Random.Range(minSpawnPos.y, maxSpawnPos.y);

                break;

            // Right
            case 1:

                xSpawnPos = Random.Range(minSpawnPos.x, maxSpawnPos.x);
                ySpawnPos = Random.Range(-maxSpawnPos.y, maxSpawnPos.y);

                break;

            // Bellow
            case 2:

                xSpawnPos = Random.Range(-maxSpawnPos.x, maxSpawnPos.x);
                ySpawnPos = Random.Range(-minSpawnPos.y, -maxSpawnPos.y);

                break;

            // Left
            case 3:

                xSpawnPos = Random.Range(-minSpawnPos.x, -maxSpawnPos.x);
                ySpawnPos = Random.Range(-maxSpawnPos.y, maxSpawnPos.y);

                break;

        }


        GameObject spawnedObject = Instantiate(enemyObj);
        spawnedObject.transform.position = new Vector2(player.transform.position.x + xSpawnPos, player.transform.position.y + ySpawnPos);

        StartCoroutine(SpawnEnemyRoutine());

    }

    IEnumerator IncreaseDifficultyRoutine()
    {

        yield return new WaitForSeconds(difficultyIncreaseInterval);


    }
}
