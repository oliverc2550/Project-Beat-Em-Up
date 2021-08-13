using System.Collections;
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
    [SerializeField] protected float m_stoppingDistance;
    [SerializeField] [Range(0, 10)] float m_patrollingDistanceX;
    [SerializeField] int m_stunCooldown = 3;
    [SerializeField] int m_PatrolWaitTime = 3;
    [SerializeField] bool m_petrolsOnStart;

    float m_startXPos;

    bool m_canBeStunned = true;
    protected string m_attackAnimation;
    public EnemyBoss summoner;
    Vector3 m_currentDirection;


    EnemyUI m_enemyUI;
    Transform m_target;
    protected EnemyState m_currentState;

    protected override void Start()
    {
        base.Start();
        SetTarget(FindObjectOfType<PlayerController>().transform);
        m_enemyUI = GetComponentInChildren<EnemyUI>();
        m_enemyUI.SetEnemyNameUI(m_EnemyName);
        m_attackAnimation = "Attack";
        m_startXPos = transform.position.x;
        m_currentDirection = Vector3.right;

        if (m_petrolsOnStart)
        {
            SetEnemyState(EnemyState.Patrol);
        }
        else
        {
            SetEnemyState(EnemyState.Chase);
        }
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
        else if (m_currentState == EnemyState.Chase && !m_animator.GetCurrentAnimatorStateInfo(0).IsName(m_attackAnimation))
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
            m_currentDirection = -(m_target.position - transform.position).normalized;
            Move(m_currentDirection);
        }
        else if (m_currentState == EnemyState.Patrol)
        {
            m_animator.SetBool("Walking", true);

            Move(m_currentDirection);

            Vector3 targetPos;

            if (m_currentDirection == Vector3.right)
            {
                targetPos = new Vector3(m_startXPos + m_patrollingDistanceX, transform.position.y, transform.position.z);
            }
            else
            {
                targetPos = new Vector3(m_startXPos - m_patrollingDistanceX, transform.position.y, transform.position.z);
            }

            float distance = Vector3.Distance(transform.position, targetPos);
            Debug.Log("D " + distance);
            if (distance < 0.5f)
            {
                StartCoroutine(WaitBeforeChangingDirection());
            }
        }
    }
    IEnumerator WaitBeforeChangingDirection()
    {
        SetEnemyState(EnemyState.Idle);

        yield return new WaitForSeconds(m_PatrolWaitTime);

        ChangeDirection();
        SetEnemyState(EnemyState.Patrol);
    }

    void ChangeDirection()
    {

        if (m_currentDirection == Vector3.right)
        {
            m_currentDirection = Vector3.left;
        }
        else
        {
            m_currentDirection = Vector3.right;
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

        m_agent.Move(direction * m_movementSpeed * Time.deltaTime);

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

        if (m_currentState == EnemyState.Patrol || m_currentState == EnemyState.Idle)
        {
            m_currentState = EnemyState.Chase;
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
            FindObjectOfType<PlayerController>().currentCharge += m_gainChargeOnEnemyDamaged;

            if (summoner.summonedEnemies.Count == 0)
            {
                summoner.SetEnemyState(EnemyState.Chase);
            }
        }
    }
}

