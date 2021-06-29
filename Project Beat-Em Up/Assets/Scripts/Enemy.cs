﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : CharacterController1
{
    [SerializeField] NavMeshAgent m_agent;

    Transform m_target;

    private void Start()
    {
       SetTarget(FindObjectOfType<Player1>().transform);
    }

    private void Update()
    {
        Vector3 direction = (m_target.position - transform.position).normalized;
        m_agent.Move(direction * m_movementSpeed);

        LookAtDirection(direction.x);
    }

    void SetTarget(Transform target)
    {
        m_target = target;
    }

}