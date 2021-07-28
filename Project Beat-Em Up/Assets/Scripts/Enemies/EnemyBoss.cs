using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyBoss : Enemy
{
    public enum BossAttacks { Summoning, DeadlySpikes, HitWithAOE }

    // [SerializeField] private Laser m_laserPrefab;
    [SerializeField] private Enemy m_enemyToSummonOnPhase1;
    [SerializeField] protected Enemy m_enemyToSummonOnPhase2;
    [SerializeField] private int m_enemyCountToSummonOnPhase1 = 2;
    [SerializeField] protected int m_enemyCountToSummonOnPhase2 = 2;

    [Tooltip("How many basic attacks will be done before the speacial attack is played.")]
    [SerializeField] private int m_minSpecialAttackHitCount = 1;
    [SerializeField] private int m_maxSpecialAttackHitCount = 10;
    [Tooltip("How much damage should be dealt in order to cancel channelling.")]

    private int m_attackCount = 0;
    private int m_lastAttack = -1;

    private bool m_enemiesFromPhase1Summoned = false;
    private bool m_phase2Entered = false;
    protected bool m_isChanneling = false;


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
            GetComponentInChildren<EnemyUI>().fillImage.color = new Color(0, 154, 255);
        }
    }


    protected override void AttackEffects(GameObject gameObject)
    {
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
                Instantiate(enemyToSummon, transform.position, Quaternion.identity);
            }
    }
    protected virtual void PlayPickedAttack(int attackToPlay)
    {
    }
}
