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
    EnemySpawner m_enemySpawner;
    GateOpener m_gateOpener;

    bool m_isTriggered = false;

    private void Start()
    {
        m_enemySpawner = FindObjectOfType<EnemySpawner>();
        m_gateOpener = GetComponent<GateOpener>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !m_isTriggered && m_gateOpener.m_hasOpened)
        {
            m_enemySpawner.StartSpawning(enemySpawnData);
            m_isTriggered = true;
        }
    }
}
