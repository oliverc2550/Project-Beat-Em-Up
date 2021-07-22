using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossAttacks { Summoning, Laser, Chanelling }

public class EnemyBoss1 : Enemy
{
   // [SerializeField] private Laser m_laserPrefab;
    [SerializeField] private Enemy m_enemyToSummonOnPhase1;
    [SerializeField] private Enemy m_enemyToSummonOnPhase2;
    [SerializeField] private int m_enemyCountToSummonOnPhase1 = 2;
    [SerializeField] private int m_enemyCountToSummonOnPhase2 = 2;

    [Tooltip("How many basic attacks will be done before the speacial attack is played.")]
    [SerializeField] private int m_specialAttackHitCount = 3;
    [Tooltip("How much damage should be dealt in order to cancel channelling.")]
    [SerializeField] private int m_damageAmountToCancelChannelling;

    private int m_attackCount = 0;
    private int m_lastAttack = -1;
    private float m_amountOfDamageTakenWhileChannelling = 0;

    private bool m_enemiesFromPhase1Summoned = false;
    private bool m_phase2Entered = false;
    private bool m_isChanneling = false;
    

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

        if (m_isChanneling)
        {
            m_amountOfDamageTakenWhileChannelling += damage;

            if (m_amountOfDamageTakenWhileChannelling > (float)m_damageAmountToCancelChannelling)
            {
                //cancel channeling
            }
        }
    }


    protected override void AttackEffects(GameObject gameObject)
    {
        base.AttackEffects(gameObject);
        if (m_phase2Entered)
        {
            if (m_attackCount < m_specialAttackHitCount)
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

    private void PlayPickedAttack(int attackToPlay)
    {
        if (attackToPlay == (int)BossAttacks.Summoning)
        {
            SummonEnemies(m_enemyToSummonOnPhase2, m_enemyCountToSummonOnPhase2);
            //becomes invulnerable while enemies are attacking
        }

        else if (attackToPlay == (int)BossAttacks.Laser)
        {
           // Instantiate(m_laserPrefab, transform.position, Quaternion.identity);
        }

        else if (attackToPlay == (int)BossAttacks.Chanelling)
        {
            //TODO: add long channeling animation with anim event in the end
            m_isChanneling = true;
        }
    }

    //animation event 

    private void SummonEnemies(Enemy enemyToSummon, int amount)
    {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(enemyToSummon, transform.position, Quaternion.identity);
            }
    }
}
