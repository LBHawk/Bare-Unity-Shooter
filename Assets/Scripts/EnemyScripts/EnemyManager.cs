using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject enemy;                // The enemy prefab to be spawned.
    public float baseSpawnTime = 3f;        // How long between each spawn at wave 1, base number for future waves.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public int baseEnemyNumber = 10;        // Number of enemies on wave 1, the base number to calculate from for future waves
    public int enemiesPerWave;              // Number of additional enemies per wave
    public float spawnPerWave;              // Number of waves to lower spawn time by 1 second (e.g. value of 20 means lower spawn time by 1/20s per wave0)
    public float spawnTimeFloor;            // Lowest spawnTime possible
    public bool spawningDone = true;



    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        //InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    // void Spawn()
    // {
    //     // If the player has no health left...
    //     if (playerHealth.currentHealth <= 0f)
    //     {
    //         // ... exit the function.
    //         return;
    //     }

    //     // Find a random index between zero and one less than the number of spawn points.
    //     int spawnPointIndex = Random.Range(0, spawnPoints.Length);

    //     // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
    //     Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    // }

    // Spawner to be called from GameManager.  We subtract 1 for enmies(spawnTime)ThisWave because we are starting
    // waves at 1, not 0 , because I don't know why
    public IEnumerator Spawner(int wave)
    {
        spawningDone = false;
        int enemiesThisWave = baseEnemyNumber + ((wave - 1) * enemiesPerWave);  // Additional 4 enemies each wave
        float spawnTimeThisWave = baseSpawnTime - ((wave - 1) / spawnPerWave);
        Mathf.Clamp(spawnTimeThisWave, spawnTimeFloor, baseSpawnTime);          // Keep spawn time above floor

        for(int i = 0; i < enemiesThisWave; i++)
        {
            // Don't spawn new enemies if player has died
            if (playerHealth.currentHealth <= 0f)
            {
                yield break;
            }

            // Get random spawn point, create instance of enemy at that point
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

            yield return new WaitForSeconds(spawnTimeThisWave);
        }

        spawningDone = true;

        yield break;
    }
}