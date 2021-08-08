﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//Changelog
/*Inital Script created by Thea
 */

public class EnemyBoss : Enemy
{
    public enum BossAttacks { Summoning, DeadlySpikes, HitWithAOE }

    // [SerializeField] private Laser m_laserPrefab;
    [SerializeField] private Enemy m_enemyToSummonOnPhase1;
    [SerializeField] protected Enemy m_enemyToSummonOnPhase2;
    [SerializeField] private int m_enemyCountToSummonOnPhase1 = 2;
    [SerializeField] protected int m_enemyCountToSummonOnPhase2 = 2;
    [SerializeField]  int m_gainedScoreOnBossEnteringPhase2 = 50;

    [Tooltip("How many basic attacks will be done before the speacial attack is played.")]
    [SerializeField] private int m_minSpecialAttackHitCount = 1;
    [SerializeField] private int m_maxSpecialAttackHitCount = 10;
    [Tooltip("How much damage should be dealt in order to cancel channelling.")]

    private int m_attackCount = 0;
    private int m_lastAttack = -1;

    private bool m_enemiesFromPhase1Summoned = false;
    private bool m_phase2Entered = false;
    protected bool m_isChanneling = false;

    public List<Enemy> summonedEnemies = new List<Enemy>();


    protected override void Start()
    {
        base.Start();
    }

    public override void OnTakeDamage(float damage)
    {
        base.OnTakeDamage(damage);

        if (IcurrentHealth / ImaxHealth < 0.75f && !m_enemiesFromPhase1Summoned)
        {
            SummonEnemies(m_enemyToSummonOnPhase1, m_enemyCountToSummonOnPhase1);
            m_enemiesFromPhase1Summoned = true;
        }
        else if (IcurrentHealth / ImaxHealth < 0.5f)
        {
            m_phase2Entered = true;
            FindObjectOfType<ScoreManager>().AddScore(m_gainedScoreOnBossEnteringPhase2);
            GetComponentInChildren<EnemyUI>().fillImage.color = new Color(0, 154, 255);
            transform.DOScale(new Vector3(4.5f,4.5f,4.5f), 2);
        }
    }


    protected override void AttackEffects(GameObject gameObject)
    {
        Debug.Log("AttackEffects");

        base.AttackEffects(gameObject);
        if (m_phase2Entered)
        {
            int specialAttackHitCount;
            specialAttackHitCount = UnityEngine.Random.Range(m_minSpecialAttackHitCount, m_maxSpecialAttackHitCount);

            if (m_attackCount < specialAttackHitCount)
            {
                m_attackCount++;
            }
            else
            {
                PickRandomAttackInPhase2();
                PlayPickedAttack(m_lastAttack);

                m_attackCount = 0;
            }
        }
    }

    private void PickRandomAttackInPhase2()
    {
        //https://stackoverflow.com/questions/856154/total-number-of-items-defined-in-an-enum
        int maxAttacks = Enum.GetNames(typeof(BossAttacks)).Length;
        int randomAttack = UnityEngine.Random.Range(0, maxAttacks);

        if (randomAttack == m_lastAttack)
        {
            randomAttack++;

            if (randomAttack == maxAttacks)
            {
                randomAttack = 0;
            }
        }

        m_lastAttack = randomAttack;
    }


    //animation event 

    public void SummonEnemies(Enemy enemyToSummon, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            float xPos;
            float zPos;
            xPos = UnityEngine.Random.value * i + transform.position.x;
            zPos = UnityEngine.Random.value * i + transform.position.z;

            Enemy enemy = Instantiate(enemyToSummon, new Vector3(xPos, transform.position.y, zPos), Quaternion.identity);

            //Every time when an enemy is summoned, set the boss to be a sumoner
            enemy.summoner = this;
            summonedEnemies.Add(enemy);
        }
    }
    protected virtual void PlayPickedAttack(int attackToPlay)
    {
    }
}
