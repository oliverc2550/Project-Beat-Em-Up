using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemywNavmesh : CombatandMovement
{
    [SerializeField] private string m_EnemyName = "Enemy";
    [SerializeField] private int m_scoreGainedOnDeath = 200;
    [SerializeField] NavMeshAgent m_agent;

    EnemyUI m_enemyUI;
    Transform m_target;
    EnemyState m_currentState;
    private float m_maxTime = 1.0f;
    private float m_minDistance;
    private float m_currentTime = 0.0f;

    protected override void Start()
    {
        base.Start();
        m_minDistance = m_agent.stoppingDistance;
        m_target = FindObjectOfType<PlayerController>().transform;
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
            m_agent.destination = transform.position;
            m_animator.SetBool("Walking", false);
        }

        //https://answers.unity.com/questions/362629/how-can-i-check-if-an-animation-is-being-played-or.html
        else if (m_currentState == EnemyState.Chase && !m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            m_currentTime -= Time.deltaTime;
            if(m_currentTime < 0.0f)
            {
                SetTarget();
                LookAtDirection(m_agent.velocity.x);
                m_currentTime = m_maxTime;
            }
            m_animator.SetBool("Walking", true);

            //Vector3 direction = (m_target.position - transform.position).normalized;
            //Move(direction);

            if (PlayerInRange())
            {
                OnPlayerInRange();
            }
        }
        else if (m_currentState == EnemyState.Run)
        {
            //run animation
            m_agent.destination = -(m_target.position - transform.position).normalized;
            m_animator.SetBool("Walking", true);
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
        return distance < m_agent.stoppingDistance;
    }

    protected virtual void OnPlayerInRange()
    {
        m_animator.SetTrigger("Attack");
    }


    void SetTarget()
    {
        float sqDistance = (m_target.position - m_agent.destination).sqrMagnitude;
        if(sqDistance > m_minDistance * m_minDistance)
        {
            m_agent.destination = m_target.position;
        }
    }

    public override void OnTakeDamage(float damage)
    {
        base.OnTakeDamage(damage);
        m_animator.SetTrigger("Stun");
        m_enemyUI.SetHealthUI(IcurrentHealth, ImaxHealth);
    }

    protected void SetEnemyState(EnemyState state)
    {
        m_currentState = state;
    }

    public override void Die()
    {
        //FindObjectOfType<EnemySpawner>().RemoveEnemy(this);
        FindObjectOfType<ScoreManager>().AddScore(m_scoreGainedOnDeath);
        base.Die();
    }
}
