using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// Changelog
/* Inital Script created by Thea (29/06/21)
 */

// Enemy state enum that the movement states of the enemy are stored.
public enum EnemyState { Idle, Chase, Run, Patrol }

public class Enemy : CombatandMovement
{
    #region Variables
    [SerializeField] protected CapsuleCollider m_collider;
    [SerializeField] GameObject m_powerUpObject;
    [SerializeField] NavMeshAgent m_agent;
    [SerializeField] Material m_enemyNonAttackingMaterial;
    [SerializeField] Material m_enemyAttackingMaterial;

    [SerializeField] private string m_EnemyName;
    [SerializeField] [Range(0, 100)] int m_chanceToDropPowerUp;
    [SerializeField] private int m_scoreGainedOnDeath = 200;
    [SerializeField] private int m_gainChargeOnEnemyDamaged = 1;
    [SerializeField] protected float m_stoppingDistance;
    [SerializeField] [Range(0, 10)] float m_patrollingDistanceX;
    [SerializeField] int m_stunCooldown = 3;
    [SerializeField] int m_PatrolWaitTime = 3;
    [SerializeField] bool m_patrolsOnStart;
    [SerializeField] bool m_idleOnStart;
    [SerializeField] protected bool m_tutorialEnemy;
    float m_startXPos;

    bool m_canBeStunned = true;
    protected bool m_alreadyAttackedOnce = false;
    protected string m_attackAnimation;
    public EnemyBoss summoner;
    protected EnemySpawner m_enemySpawner;
    Vector3 m_currentDirection;


    EnemyUI m_enemyUI;
    Transform m_target;
    protected EnemyState m_currentState;
    #endregion

    protected override void Start()
    {
        base.Start();
        SetTarget(FindObjectOfType<PlayerController>().transform);
        m_enemyUI = GetComponentInChildren<EnemyUI>();
        m_enemySpawner = FindObjectOfType<EnemySpawner>();
        m_enemyUI.SetEnemyNameUI(m_EnemyName);
        m_attackAnimation = "Attack";
        m_startXPos = transform.position.x;
        m_currentDirection = Vector3.right;

        if (m_patrolsOnStart)
        {
            SetEnemyState(EnemyState.Patrol);
        }
        else if (m_idleOnStart)
        {
            SetEnemyState(EnemyState.Idle);
        }
        else
        {
            SetEnemyState(EnemyState.Chase);
        }
    }

    // Checks which state the enemy is currently in and moves the enemy accordingly.
    protected virtual void Update()
    {

        // Don't read the update while the stun animation is running
        // How to check if an animation is being played: https://answers.unity.com/questions/362629/how-can-i-check-if-an-animation-is-being-played-or.html
        if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Stun"))
        {
            return;
        }

        // During Idle state, enemy stays idle and does nothing.
        if (m_currentState == EnemyState.Idle)
        {
            m_animator.SetBool("Walking", false);
            m_spriteRenderer.material = m_enemyNonAttackingMaterial;
        }

        // During the chase state, enemy chases the player. When the enemy gets close, it starts attacking.
        // To prevent enemy from moving while attacking, it also checks if the attack animation is not played at the same time.
        else if (m_currentState == EnemyState.Chase && !m_animator.GetCurrentAnimatorStateInfo(0).IsName(m_attackAnimation))
        {
            m_animator.SetBool("Walking", true);
            m_spriteRenderer.material = m_enemyAttackingMaterial;
            Vector3 direction = (m_target.position - transform.position).normalized;
            Move(direction);

            if (PlayerInRange())
            {
                OnPlayerInRange();
            }
        }
        // During run state, enemy runs away from the player.
        else if (m_currentState == EnemyState.Run)
        {
            m_animator.SetBool("Walking", true);
            m_currentDirection = -(m_target.position - transform.position).normalized;
            Move(m_currentDirection);

            m_spriteRenderer.material = m_enemyAttackingMaterial;
        }

        // During patrolling state, enemy picks a random direction and moves in that direction.
        // Once the enemy reaches to max patrolling distance, it waits some seconds, then it changes its direction and repeats this cycle forever.
        else if (m_currentState == EnemyState.Patrol)
        {
            m_spriteRenderer.material = m_enemyNonAttackingMaterial;
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
            if (distance < 0.5f)
            {
                StartCoroutine(WaitBeforeChangingDirection());
            }
        }
    }
    // This coroutine stops the movement of a patrolling enemy, only to go back to patrolling after the patrol wait time has passed.
    // It is called when the enemy reaches to final destination while patrolling.
    IEnumerator WaitBeforeChangingDirection()
    {
        SetEnemyState(EnemyState.Idle);

        yield return new WaitForSeconds(m_PatrolWaitTime);

        ChangeDirection();
        SetEnemyState(EnemyState.Patrol);
    }

    // Changes the direction of the enemy from left to right or vice versa. Is called when a patrolling enemy reaches to its destination and waits some seconds.
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

    // This is called during the chase state to check if the player is in the range of this enemy.
    // It checks to see if the remaining distance is less than the stopping distance of this enemy and returns true or false accordingly.
    protected bool PlayerInRange()
    {
        float distance = Vector3.Distance(m_target.position, transform.position);
        return distance < m_stoppingDistance;
    }

    // This is called while the player is in range of this enemy.
    protected virtual void OnPlayerInRange()
    {
        m_animator.SetTrigger(m_attackAnimation);
        m_alreadyAttackedOnce = true;
    }

    // This function moves the nav mesh agennt of the enemy in a direction.
    protected override void Move(Vector3 direction)
    {
        if (m_isBlocking == false)
        {
            base.Move(direction);

            m_agent.Move(direction * m_movementSpeed * Time.deltaTime);

            LookAtDirection(-direction.x);

            if (m_tutorialEnemy)
            {
                LookAtDirection(direction.x);
            }
        }
    }

    // This sets the target of this enemy. The enemy chases that target during chase state.
    void SetTarget(Transform target)
    {
        m_target = target;
    }

    // This coroutine is called when the enemy is stuned.
    // It waits for the cooldown duration, and then allows the enemy to be stunned again in order to prevent chain stunning.
    IEnumerator StunCooldown()
    {
        m_canBeStunned = false;
        yield return new WaitForSeconds(m_stunCooldown);
        m_canBeStunned = true;
    }

    // This overrides the TakeDamage function of CombatAndMovement and it comes from IDamageable interface. It is called when this enemy takes damage.
    // It stuns the enemy and attracts this enemy to the player if the enemy was patrolling or idle.
    public override void OnTakeDamage(float damage)
    {
        base.OnTakeDamage(damage);

        if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Stun") && m_canBeStunned)
        {
            m_animator.SetTrigger("Stun");
            StartCoroutine(StunCooldown());
        }

        if (m_currentState == EnemyState.Patrol || m_currentState == EnemyState.Idle)
        {
            m_currentState = EnemyState.Chase;
        }

        m_enemyUI.SetHealthUI(IcurrentHealth, ImaxHealth);
    }

    // This is called to change the enemy state.
    protected void SetEnemyState(EnemyState state)
    {
        m_currentState = state;
    }

    // This overrides the Die function of CombatAndMovement and it comes from IDamageable interface. It is called when this enemies health reaches to 0.
    // It spawns a powerup object by a chance and adds score to the player.
    public override void Die()
    {
        float chance = (float)m_chanceToDropPowerUp / 100; 
        if (Random.value < chance)
        {
            Instantiate(m_powerUpObject);
        }



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

        Destroy(gameObject);
    }


}

