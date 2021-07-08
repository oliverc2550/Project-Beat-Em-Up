using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Enemy[] m_enemyTypesToSummon;
    [SerializeField] private float m_minSpawnTime = 0.3f;
    [SerializeField] private float m_maxSpawnTime = 2f;
    [SerializeField] private int m_maxEnemyCount = 10;
    [SerializeField] private Transform m_leftSpawnPoint;
    [SerializeField] private Transform m_RightSpawnPoint;

    public List<Enemy> spawnedEnemies = new List<Enemy>();

    private const float m_minZ = -6;
    private const float m_maxZ = 7;

    private void Start()
    {
        StartCoroutine(Summoning());
    }


    // https://answers.unity.com/questions/503932/how-to-cycle-coroutine-in-c-say-every-1-sec.html
    IEnumerator Summoning()
    {
        while (true)
        {
            if (spawnedEnemies.Count < m_maxEnemyCount)
            {
                float xPos;
                if (Random.value < 0.5f)
                {
                    xPos = m_leftSpawnPoint.position.x;
                }
                else
                {
                    xPos = m_RightSpawnPoint.position.x;
                }
                Vector3 position = new Vector3(xPos, 0, Random.Range(m_minZ, m_maxZ));

                SummonEnemy(m_enemyTypesToSummon[Random.Range(0, m_enemyTypesToSummon.Length)], position);

            }
            yield return new WaitForSeconds(Random.Range(m_minSpawnTime, m_maxSpawnTime));
        }
    }

    private void SummonEnemy(Enemy enemyPrefab, Vector3 position)
    {
        Enemy enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        spawnedEnemies.Add(enemy);
    }
}
