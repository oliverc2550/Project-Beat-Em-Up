using UnityEngine;
using UnityEngine.AI;

// Changelog
/* Inital Script created by Thea (29/06/21)
 */
public enum EnemyState { Idle, Chase, Run, Patrol }

public class Enemy : CombatandMovement
{
    [SerializeField]private string m_EnemyName = "Enemy";
    [SerializeField]private int m_scoreGainedOnDeath = 200;
    [SerializeField] NavMeshAgent m_agent;
    [SerializeField] private float m_stoppingDistance = 1.5f;

    EnemyUI m_enemyUI;
    Transform m_target;
    EnemyState m_currentState;

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

        LookAtDirection(direction.x);
    }

    void SetTarget(Transform target)
    {
        m_target = target;
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
        FindObjectOfType<EnemySpawner>().RemoveEnemy(this);
        FindObjectOfType<ScoreManager>().AddScore(m_scoreGainedOnDeath);
        base.Die();
    }
}

