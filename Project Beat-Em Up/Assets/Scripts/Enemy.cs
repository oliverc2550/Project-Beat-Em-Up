using UnityEngine;
using UnityEngine.AI;

// Changelog
/* Inital Script created by Thea (29/06/21)
 */
public enum EnemyState { Idle, Chase, Run, Patrol }

public class Enemy : CombatandMovement
{
    [SerializeField] NavMeshAgent m_agent;

    Transform m_target;
    EnemyState m_currentState;

    protected override void Start()
    {
        base.Start();
        SetTarget(FindObjectOfType<PlayerController>().transform);
        SetEnemyState(EnemyState.Chase);
    }

    protected virtual void Update()
    {
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

            float distance = Vector3.Distance(m_target.position, transform.position);
            if (distance < 1.5f)
            {
                m_animator.SetTrigger("Attack");
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

    protected override void Move(Vector3 direction)
    {
        base.Move(direction);

        m_agent.Move(direction * m_movementSpeed);

        LookAtDirection(direction.x);
    }

    void SetTarget(Transform target)
    {
        m_target = target;
    }

    protected void SetEnemyState(EnemyState state)
    {
        m_currentState = state;
    }
}

