using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnDirection {Left, Right}


[System.Serializable]
public struct EnemySpawnData
{
    public int basicEnemiesToSpawnCount;
    public int thiefEnemiesToSpawnCount;
    public int summonerEnemiesToSpawnCount;
    public int tankEnemiesToSpawnCount;
    public int bossEnemiesToSpawnCount;

    public SpawnDirection spawnDirection;
}


public class EnemyWaveTrigger : MonoBehaviour
{
    [SerializeField] EnemySpawnData enemySpawnData;
    EnemySpawner enemySpawner;

    bool m_isTriggered = false;

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&!m_isTriggered)
        {
            enemySpawner.StartSpawning(enemySpawnData);
            m_isTriggered = true;
        }
    }
}
