﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : Enemy
{
    [Header("Tank Settings")]
    [SerializeField] [Range(0, 100)] private float m_chanceToThrowProjectile = 30;

    //protected override void AttackEffects(GameObject gameObject)
    //{
    //    base.AttackEffects(gameObject);

    //    if (Random.value < m_chanceToThrowProjectile / 100)
    //    {
    //        //TODO: replace this with animation 
    //        m_animator.SetTrigger("Jump");
    //        Debug.Log("DEBUG");
    //    }
    //}
    protected override void Start()
    {
        base.Start();
        GetComponentInChildren<TankProjectile>().damageToDeal = m_normalAttackDamage;
        m_attackAnimation = "LeftAttack";
    }


    protected override void OnPlayerInRange()
    {
        // if the direction is left
        if (m_spriteRenderer.flipX)
        {
            m_attackAnimation = "LeftAttack";
        }
        else
        {
            m_attackAnimation = "RightAttack";
        }

        m_animator.SetTrigger(m_attackAnimation);
    }


}
