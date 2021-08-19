using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changelog
/*Script created by Thea
 * 19.08.2021 - Oliver - Added sound
 */

public class Lv1Boss : EnemyBoss
{
    #region Variables
    [SerializeField] public float m_aoeRange;
    [SerializeField] private float m_aoeDamage;
    [SerializeField] private SpikesTimer m_spikesTimerPrefab;
    [SerializeField] SphereCollider m_rangeCollider;
    public List<SpikesTimer> aliveTimers = new List<SpikesTimer>();

    private const int m_spikesTimerCount = 3;
    #endregion

    #region Combat
    // It is called when the boss enters phase 2. Plays an attack from a randomly given index by triggering the respective animation.
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

    // This function is called when onCameraSwitched event is invoked in inspector. It plays an animation only once when the camera focuses on this enemy.
    public void PlayAttackWhenBossAppears()
    {
        m_animator.SetTrigger("AOE Attack");
    }

    // It is called every time the enemy takes damage.
    public override void OnTakeDamage(float damage)
    {
        base.OnTakeDamage(damage);

        if (m_currentState == EnemyState.Idle)
        {
            m_currentState = EnemyState.Chase;
        }
    }
    #endregion

    #region Animation events
    // This is an animation event during box spawn animation.
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

    // This is an animation event called during Slam Attack animation
    private void UseAreaOfEffect()
    {
        AudioManager.Instance.Play("BossSlamAttackSFX");
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
    #endregion
}
