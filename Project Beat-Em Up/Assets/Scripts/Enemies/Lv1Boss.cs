using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*Inital Script created by Thea
 */

public class Lv1Boss : EnemyBoss
{

    [SerializeField] public float m_aoeRange;
    [SerializeField] private float m_aoeDamage;
    [SerializeField] private SpikesTimer m_spikesTimerPrefab;
    public List<SpikesTimer> aliveTimers = new List<SpikesTimer>();

    private const int m_spikesTimerCount = 3;

    protected override void Start()
    {
        base.Start();
        PlayPickedAttack(1);
    }

    protected override void PlayPickedAttack(int attackToPlay)
    {
        base.PlayPickedAttack(attackToPlay);

        if (attackToPlay == (int)BossAttacks.Summoning)
        {
            m_enemyToSummon = m_enemyToSummonOnPhase2;
            m_amountOfEnemiesToSummon = m_enemyCountToSummonOnPhase2;

            m_animator.SetTrigger("Summon");
            SetEnemyState(EnemyState.Idle);

        }

        else if (attackToPlay == (int)BossAttacks.DeadlySpikes)
        {
            m_animator.SetTrigger("Throw");
        }

        else if (attackToPlay == (int)BossAttacks.HitWithAOE)
        {
            m_animator.SetTrigger("AOE Attack");
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

    //anim evet
    private void InstantiateSpikesTimers()
    {
        for (int i = 0; i < m_spikesTimerCount; i++)
        {
            SpikesTimer spikesTimer = Instantiate(m_spikesTimerPrefab, transform.position, Quaternion.identity);
            aliveTimers.Add(spikesTimer);
            spikesTimer.enemyBoss = this;
            //aliveTimers[i].destroyedBoxesByTimer = aliveTimers;
        }

        aliveTimers[0].ThrowSpikesTimer(Vector3.left);
        aliveTimers[1].ThrowSpikesTimer(Vector3.right);
        aliveTimers[2].ThrowSpikesTimer(Vector3.zero);
    }

    //anim event 
    private void UseAreaOfEffect()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, m_aoeRange, m_targetLayer);
        foreach (Collider nearbyObject in colliders)
        {
            // Checking if the nearby objects have damageable interface. If they do, they receive damage.
            IDamagable damagableTarget = nearbyObject.gameObject.GetComponent<IDamagable>();
            if (damagableTarget != null)
            {
                damagableTarget.TakeDamage(m_aoeDamage);
            }
        }
    }
}
