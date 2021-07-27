using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : Enemy
{
    [Header("Tank Settings")]
    [SerializeField] [Range(0, 100)] private float m_chanceToUseAreaOfEffect = 30;
    [SerializeField] private float m_aoeRange;
    [SerializeField] private float m_aoeDamage;


    protected override void AttackEffects(GameObject gameObject)
    {
        base.AttackEffects(gameObject);

        if (Random.value < m_chanceToUseAreaOfEffect / 100)
        {
            //TODO: replace this with animation 
            m_animator.SetTrigger("Jump");
            UseAreaOfEffect(m_aoeRange, m_aoeDamage);
        }
    }

}
