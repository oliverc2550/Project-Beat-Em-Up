﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*Inital Script created by Thea
 */

public class Lv1Boss : EnemyBoss
{

    [SerializeField] private float m_aoeRange;
    [SerializeField] private float m_aoeDamage;
    [SerializeField] private SpikesTimer m_spikesTimerPrefab;
    private List<SpikesTimer> spikeTimers = new List<SpikesTimer>();

    private const int m_spikesTimerCount = 3;



    protected override void PlayPickedAttack(int attackToPlay)
    {
        Debug.Log("PlayPicked attack");

        base.PlayPickedAttack(attackToPlay);

        if (attackToPlay == (int)BossAttacks.Summoning)
        {
            m_animator.SetTrigger("Summon");
            SetEnemyState(EnemyState.Idle);
            SummonEnemies(m_enemyToSummonOnPhase2, m_enemyCountToSummonOnPhase2);
            //becomes invulnerable while enemies are attacking
        }

        else if (attackToPlay == (int)BossAttacks.DeadlySpikes)
        {
            m_animator.SetTrigger("Throw");
            Debug.Log("Attack is played");
            for (int i = 0; i < m_spikesTimerCount; i++)
            {
                spikeTimers.Add(Instantiate(m_spikesTimerPrefab, transform.position, Quaternion.identity));
                spikeTimers[i].allTimers = spikeTimers;
            }

            spikeTimers[0].ThrowSpikesTimer(Vector3.left);
            spikeTimers[1].ThrowSpikesTimer(Vector3.right);
            spikeTimers[2].ThrowSpikesTimer(Vector3.zero);
        }

        else if (attackToPlay == (int)BossAttacks.HitWithAOE)
        {
            //TODO: replace this with animation 
            m_animator.SetTrigger("Attack");
            transform.DOShakeScale(1);
            UseAreaOfEffect(m_aoeRange, m_aoeDamage);
        }
    }

    public override void OnTakeDamage(float damage)
    {
        base.OnTakeDamage(damage);
        if (m_currentState == EnemyState.Idle)
        {
            m_currentState = EnemyState.Chase;
        }
    }


    //used by the enemy tank and the boss lv1
    //anim event NOT ADDED TO ANIMATION YET
    protected void UseAreaOfEffect(float aoeRange, float aoeDamage)
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, aoeRange, m_targetLayer);
        foreach (Collider nearbyObject in colliders)
        {
            // Checking if the nearby objects have damageable interface. If they do, they receive damage.
            IDamagable damagableTarget = nearbyObject.gameObject.GetComponent<IDamagable>();
            if (damagableTarget != null)
            {
                damagableTarget.TakeDamage(aoeDamage);
            }
        }
    }
}
