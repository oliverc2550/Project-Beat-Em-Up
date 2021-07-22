using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.youtube.com/watch?v=aPXvoWVabPY

[CreateAssetMenu(fileName = "New Enemy Wave Data", menuName = "Enemies/New Wave" )]
public class EnemyWaveData : ScriptableObject
{
    public int basicEnemiesToSpawnCount;
    public int thiefEnemiesToSpawnCount;
    public int summonerEnemiesToSpawnCount;
    public int tankEnemiesToSpawnCount;
    public int bossEnemiesToSpawnCount;
}
