using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changelog
/*Inital Script created by Thea (7/07/21)
 */

//how to use structs: https://www.tutorialsteacher.com/csharp/csharp-struct
//how to serialize structs: https://forum.unity.com/threads/initialze-array-struct-variables-within-unity-editor.44473/
[System.Serializable]
public struct EnemyToSpawn
{
    public Enemy enemyPrefab;
    [Tooltip("The minimum amount of seconds that will pass until the next enemy is spawned")]
    public float minSpawnTime;
    [Tooltip("The maximum amount of seconds that will pass until the next enemy is spawned")]
    public float maxSpawnTime;
    public List<Enemy> spawnedEnemies;
}


public class EnemySpawner : MonoBehaviour
{
    [Header("Settings")]

    [SerializeField] EnemyToSpawn BasicEnemy;
    [SerializeField] EnemyToSpawn ThiefEnemy;
    [SerializeField] EnemyToSpawn SummonerEnemy;
    [SerializeField] EnemyToSpawn TankEnemy;
    [SerializeField] EnemyToSpawn BossEnemy;
    [SerializeField] private Transform m_leftSpawnPoint;
    [SerializeField] private Transform m_RightSpawnPoint;

    public int enemyCount = 0;

    [Header("WARNING: Avoid Editing")]
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer + TESTING.")]
    public float m_minZ = -1.6f;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer + TESTING.")]
    public float m_maxZ = -11.5f;

    public void StartSpawning(EnemySpawnData data)
    {
        StartCoroutine(Spawning(data.basicEnemiesToSpawnCount, data.spawnDirection, BasicEnemy));
        StartCoroutine(Spawning(data.summonerEnemiesToSpawnCount, data.spawnDirection, SummonerEnemy));
        StartCoroutine(Spawning(data.tankEnemiesToSpawnCount, data.spawnDirection, TankEnemy));
        StartCoroutine(Spawning(data.thiefEnemiesToSpawnCount, data.spawnDirection, ThiefEnemy));
        StartCoroutine(Spawning(data.bossEnemiesToSpawnCount, data.spawnDirection, BossEnemy));
    }

    IEnumerator Spawning(int enemyAmount, SpawnDirection direction, EnemyToSpawn enemyToSpawn)
    {
        int enemiesSpawned = 0; 

        while (enemiesSpawned < enemyAmount)
        {
            yield return new WaitForSeconds(Random.Range(enemyToSpawn.minSpawnTime, enemyToSpawn.maxSpawnTime));

            enemiesSpawned++;
            float xPos;
            if (direction == SpawnDirection.Left)
            {
                xPos = m_leftSpawnPoint.position.x;
            }
            else
            {
                xPos = m_RightSpawnPoint.position.x;
            }

            Vector3 position = new Vector3(xPos, 0, Random.Range(m_minZ, m_maxZ));

            SummonEnemy(enemyToSpawn, position);

            Debug.Log("EnemySpawned: " + enemyToSpawn.enemyPrefab.name + enemiesSpawned);
        
        }
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemyCount--;

        for (int i = 0; i < BasicEnemy.spawnedEnemies.Count; i++)
        {
            if (enemy == BasicEnemy.spawnedEnemies[i])
            {
                BasicEnemy.spawnedEnemies.Remove(enemy);
                return;
            }
        }
        for (int i = 0; i < ThiefEnemy.spawnedEnemies.Count; i++)
        {
            if (enemy == ThiefEnemy.spawnedEnemies[i])
            {
                ThiefEnemy.spawnedEnemies.Remove(enemy);
                return;
            }
        }
        for (int i = 0; i < SummonerEnemy.spawnedEnemies.Count; i++)
        {
            if (enemy == SummonerEnemy.spawnedEnemies[i])
            {
                SummonerEnemy.spawnedEnemies.Remove(enemy);
                return;
            }
        }
        for (int i = 0; i < TankEnemy.spawnedEnemies.Count; i++)
        {
            if (enemy == TankEnemy.spawnedEnemies[i])
            {
                TankEnemy.spawnedEnemies.Remove(enemy);
                return;
            }
        }
    }

    private void SummonEnemy(EnemyToSpawn enemyToSpawn, Vector3 position)
    {
        Enemy enemy = Instantiate(enemyToSpawn.enemyPrefab, position, Quaternion.identity);
        enemyToSpawn.spawnedEnemies.Add(enemy);
        enemyCount++;
    }
}
