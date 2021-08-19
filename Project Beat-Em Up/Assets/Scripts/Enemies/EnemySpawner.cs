using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changelog
/*Script created by Thea
 */

#region Struct
// This struct is created to pack enemy information to make it easier for the designer.
// How to use structs: https://www.tutorialsteacher.com/csharp/csharp-struct
// How to serialize structs: https://forum.unity.com/threads/initialze-array-struct-variables-within-unity-editor.44473/
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
#endregion

public class EnemySpawner : MonoBehaviour
{
    #region Variables
    [Header("Settings")]
    [SerializeField] EnemyToSpawn BasicEnemy;
    [SerializeField] EnemyToSpawn ThiefEnemy;
    [SerializeField] EnemyToSpawn SummonerEnemy;
    [SerializeField] EnemyToSpawn TankEnemy;
    [SerializeField] EnemyToSpawn BossEnemy;


    [Header("Do NOT change")]
    public int enemyCount = 0;
    public float m_minZ = 13.22f;
    public float m_maxZ = 33.63f;

    [SerializeField] private Transform m_leftSpawnPoint;
    [SerializeField] private Transform m_RightSpawnPoint;
    #endregion

    #region Spawning
    // This is called when the player enters an EnemyWaveTrigger. It starts spawning these enemies based on the given EnemySpawnData
    public void StartSpawning(EnemySpawnData data)
    {
        StartCoroutine(Spawning(data.basicEnemiesToSpawnCount, data.spawnDirection, BasicEnemy, data.leftBound, data.rightBound));
        StartCoroutine(Spawning(data.summonerEnemiesToSpawnCount, data.spawnDirection, SummonerEnemy, data.leftBound, data.rightBound));
        StartCoroutine(Spawning(data.tankEnemiesToSpawnCount, data.spawnDirection, TankEnemy, data.leftBound, data.rightBound));
        StartCoroutine(Spawning(data.thiefEnemiesToSpawnCount, data.spawnDirection, ThiefEnemy, data.leftBound, data.rightBound));
        StartCoroutine(Spawning(data.bossEnemiesToSpawnCount, data.spawnDirection, BossEnemy, data.leftBound, data.rightBound));
    }

    // This coroutine spawns x amount of enemyToSpawn in left or right direction of the player between bounds.
    IEnumerator Spawning(int enemyAmount, SpawnDirection direction, EnemyToSpawn enemyToSpawn, float leftBound, float rightBound)
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

            //Restricting the Xpos of the enemies while spawning because they used to spawn on the other side of the gate if their Xpos wasnt restricted
            //https://docs.unity3d.com/ScriptReference/Mathf.Clamp.html
            xPos = Mathf.Clamp(xPos, rightBound, leftBound);

            Vector3 position = new Vector3(xPos, 0, Random.Range(m_minZ, m_maxZ));

            SummonEnemy(enemyToSpawn, position);

            //Debug.Log("EnemySpawned: " + enemyToSpawn.enemyPrefab.name + enemiesSpawned);

        }
    }

    // Spawns the enemy at given position.
    private void SummonEnemy(EnemyToSpawn enemyToSpawn, Vector3 position)
    {
        Enemy enemy = Instantiate(enemyToSpawn.enemyPrefab, position, transform.rotation * Quaternion.Euler(0f, 180f, 0f));
        enemyToSpawn.spawnedEnemies.Add(enemy);
        enemyCount++;
    }
    #endregion

    #region On dead enemy
    // This is called when an enemy dies so that it can be removed from spawned enemies list.
    // It checks every spawned enemy from their lists to see if this is the correct enemy.
    // When the correct enemy is found, it removes it from the list and returns to save performance.
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
    #endregion

}
