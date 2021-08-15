using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : Enemy
{
    [Header("Tank Settings")]
    [SerializeField] [Range(0, 100)] private float m_chanceToThrowProjectile = 30;

    protected override void Start()
    {
      //  FindObjectOfType<PlayerController>().onNormalAttackEvent.AddListener();
        base.Start();
        GetComponentInChildren<TankProjectile>().damageToDeal = m_normalAttackDamage;
        m_attackAnimation = "LeftAttack";
    }

    public override void OnTakeDamage(float damage)
    {
        int chance = 50;

        if (Random.value < chance)
        {
            base.OnTakeDamage(damage);
        }

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
