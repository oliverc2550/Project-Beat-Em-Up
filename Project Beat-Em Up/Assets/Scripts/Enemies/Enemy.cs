﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// Changelog
/* Inital Script created by Thea (29/06/21)
 */
public enum EnemyState { Idle, Chase, Run, Patrol }

public class Enemy : CombatandMovement
{
    [SerializeField] protected CapsuleCollider m_collider;
    [SerializeField] NavMeshAgent m_agent;

    [SerializeField] private string m_EnemyName;
    [SerializeField] private int m_scoreGainedOnDeath = 200;
    [SerializeField] private int m_gainChargeOnEnemyDamaged = 1;
    [SerializeField] float m_stoppingDistance;
    [SerializeField] int m_stunCooldown = 3;
    bool m_canBeStunned = true;

    public EnemyBoss summoner;


    EnemyUI m_enemyUI;
    Transform m_target;
    protected EnemyState m_currentState;

    protected override void Start()
    {
        base.Start();
        SetTarget(FindObjectOfType<PlayerController>().transform);
        SetEnemyState(EnemyState.Chase);
        m_enemyUI = GetComponentInChildren<EnemyUI>();
        m_enemyUI.SetEnemyNameUI(m_EnemyName);

    }

    protected virtual void Update()
    {
        //don't read the update while the stun animation is running
        if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Stun"))
        {
            return;
        }

        if (m_currentState == EnemyState.Idle)
        {
            m_animator.SetBool("Walking", false);
        }

        //https://answers.unity.com/questions/362629/how-can-i-check-if-an-animation-is-being-played-or.html
        else if (m_currentState == EnemyState.Chase && !m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            m_animator.SetBool("Walking", true);

            Vector3 direction = (m_target.position - transform.position).normalized;
            Move(direction);

            if (PlayerInRange())
            {
                OnPlayerInRange();
            }
        }
        else if (m_currentState == EnemyState.Run)
        {
            //run animation
            m_animator.SetBool("Walking", true);
            Vector3 direction = -(m_target.position - transform.position).normalized;
            Move(direction);
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



    protected bool PlayerInRange()
    {
        float distance = Vector3.Distance(m_target.position, transform.position);
        return distance < m_stoppingDistance;
    }

    protected virtual void OnPlayerInRange()
    {
        m_animator.SetTrigger("Attack");
    }

    protected override void Move(Vector3 direction)
    {
        base.Move(direction);

        m_agent.Move(direction * m_movementSpeed*Time.deltaTime);

        LookAtDirection(-direction.x);
    }

    void SetTarget(Transform target)
    {
        m_target = target;
    }

    IEnumerator StunCooldown()
    {
        m_canBeStunned = false;
        yield return new WaitForSeconds(m_stunCooldown);
        m_canBeStunned = true;
    }

    public override void OnTakeDamage(float damage)
    {
        base.OnTakeDamage(damage); 
        
        if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Stun") && m_canBeStunned)
        {
            m_animator.SetTrigger("Stun");
            StartCoroutine(StunCooldown());
            Debug.Log("stunned");
        }

        m_enemyUI.SetHealthUI(IcurrentHealth, ImaxHealth);
       // Debug.Log("health: " + IcurrentHealth);
    }

    protected void SetEnemyState(EnemyState state)
    {
        m_currentState = state;
    }

    public override void Die()
    {
        FindObjectOfType<EnemySpawner>().RemoveEnemy(this);
        FindObjectOfType<ScoreManager>().AddScore(m_scoreGainedOnDeath);
        base.Die();

        //Every time when enemy dies it checks if it has summoner, if it has it sets the summoner's state to chase. For example when the boss summons an enemy, the boss
        //becomes a summoner of this enemy so that when that enemy is killed, the boss is switching to a different state
        if (summoner != null)
        {
            summoner.summonedEnemies.Remove(this);
            FindObjectOfType<PlayerController>().m_currentCharge += m_gainChargeOnEnemyDamaged;

            if (summoner.summonedEnemies.Count == 0)
            {
                summoner.SetEnemyState(EnemyState.Chase);
            }
        }
    }
}

