using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EnemyToSpawn
{
    public GameObject prefab;
    public Transform spawnPoint;
    public int limit;
    public int count;

}
public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public float spawnTime = 5f;            // How long between each spawn.
    public Analyst analyst;

    public int currentEnemy = 0;
    public static int totalEnemy;
    public GameObject EnemyGroup;
    public EnemyToSpawn zomBear;
    public EnemyToSpawn zomBunny;
    // public bool finishedSpawning;


    void Awake ()
    {
        totalEnemy = zomBear.limit + zomBunny.limit;
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
    }

    void Start ()
    {
        InvokeRepeating ("Spawn", 0.5f, spawnTime);

	}

    void Spawn ()
    {
        // If the player has no health left...
        if(playerHealth.currentHealth <= 0f)
        {
            // ... exit the function.
            return;
        }

        SpawnEnemy(zomBunny);
        SpawnEnemy(zomBear);

    }

    void SpawnEnemy(EnemyToSpawn enemyToSpawn)
    {
        // bool finishedSpawning = false;
        if(enemyToSpawn.count < enemyToSpawn.limit)
        {
            GameObject enemySpawned = Instantiate(enemyToSpawn.prefab, enemyToSpawn.spawnPoint.position, enemyToSpawn.spawnPoint.rotation) as GameObject;

            enemySpawned.transform.parent = GameObject.Find("EnemyGroup").transform;
            analyst.enemies.Add(enemySpawned.gameObject);
            enemyToSpawn.count++;
            currentEnemy++;
        }
    }
}