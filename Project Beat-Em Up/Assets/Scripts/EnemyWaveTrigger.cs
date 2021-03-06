using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Changelog
/*Inital Script created by Thea
 */

#region Enum and Struct
// Enum that stores left and right in order to give the designer the option to choose which direction enemies should be spawning from.
public enum SpawnDirection {Left, Right}

// This struct is created in order to pack spawn settings for each enemy together and make it cleaner.
[System.Serializable]
public struct EnemySpawnData
{
    public int basicEnemiesToSpawnCount;
    public int thiefEnemiesToSpawnCount;
    public int summonerEnemiesToSpawnCount;
    public int tankEnemiesToSpawnCount;
    public int bossEnemiesToSpawnCount;

    public SpawnDirection spawnDirection;
    public float leftBound;
    public float rightBound;
}

#endregion

public class EnemyWaveTrigger : MonoBehaviour
{
    #region Variables
    [SerializeField] EnemySpawnData enemySpawnData;
    EnemySpawner m_enemySpawner;
    //GateOpener m_gateOpener;

    bool m_isTriggered = false;
    #endregion

    #region Spawning
    private void Start()
    {
        m_enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    // When the player enters this trigger, start spawning with enemySpawnData.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !m_isTriggered)
        {
            m_enemySpawner.StartSpawning(enemySpawnData);
            m_isTriggered = true;
        }
    }
    #endregion
}
