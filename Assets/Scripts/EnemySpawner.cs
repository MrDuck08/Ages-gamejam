using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Singleton;
    
    private GameObject player;
    [SerializeField] private GameObject enemyObj;

    [SerializeField] private Vector2 minSpawnPos = Vector2.zero;
    [SerializeField] private Vector2 maxSpawnPos = Vector2.zero;

    public bool pauseExecution;
    
    private float enemySpawnInterval = 0.75f;
    private float enemySpawnTimer;
    
    private int maxEnemySpawn = 10;
    public int decreaseChanceForSpawn = 0;

    private float difficultyIncreaseInterval = 15;
    private float difficultyTimer;

    private void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }

        Singleton = this;
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (player == null || pauseExecution) return;
        
        SpawnEnemyRoutine();
        IncreaseDifficultyRoutine();
    }

    private void SpawnEnemyRoutine()
    {
        enemySpawnTimer += Time.deltaTime;
        if (enemySpawnTimer < enemySpawnInterval) return;
        
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
                ySpawnPos = Random.Range(-maxSpawnPos.y, -minSpawnPos.y);
                break;
            // Left
            case 3:
                xSpawnPos = Random.Range(-maxSpawnPos.x, -minSpawnPos.x);
                ySpawnPos = Random.Range(-maxSpawnPos.y, maxSpawnPos.y);
                break;
        }
        
        GameObject spawnedObject = Instantiate(enemyObj);
        spawnedObject.transform.position = new Vector2(player.transform.position.x + xSpawnPos, player.transform.position.y + ySpawnPos);
        enemySpawnTimer = 0f;
    }

    private void IncreaseDifficultyRoutine()
    {
        difficultyTimer += Time.deltaTime;
        if (difficultyTimer < difficultyIncreaseInterval) return;
        
        if(enemySpawnInterval > 0.20f)
        {
            enemySpawnInterval -= 0.05f;
            decreaseChanceForSpawn += 2;
                
            difficultyTimer = 0;
        }   
    }
}
