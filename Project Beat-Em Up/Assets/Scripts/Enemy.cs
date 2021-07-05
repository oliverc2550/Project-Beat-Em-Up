using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] EnemyState m_currentState;

    private void Start()
    {
        SetTarget(FindObjectOfType<PlayerController>().transform);
        SetEnemyState(EnemyState.Idle);

        // TBD: do enemies jump? 
        m_animator.SetBool("Grounded", true);
    }

    private void Update()
    {
        if (m_currentState == EnemyState.Idle)
        {
            //todo: set animations
            m_animator.SetInteger("AnimState", 0);
        }
        else if (m_currentState == EnemyState.Chase)
        {
            //run animation
            m_animator.SetInteger("AnimState", 1);
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
            m_animator.SetInteger("AnimState", 1);
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

    void SetEnemyState(EnemyState state)
    {
        m_currentState = state;
    }
}

