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
        if (Random.value < m_chanceToUseAreaOfEffect / 100)
        {
            //TODO: replace this with animation 
            UseAreaOfEffect();
        }
    }

    //anim event NOT ADDED TO ANIMATION YET
    private void UseAreaOfEffect()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, m_aoeRange, m_targetLayer); //Get an array of colliders using Physics.OverlapSphere
        foreach (Collider nearbyObject in colliders) //Iterate over each collider in the list
        {
            // Checking if the nearby objects have damageable interface. If they do, they receive damage.
            IDamagable damagableTarget = nearbyObject.gameObject.GetComponent<IDamagable>();
            if (damagableTarget != null)
            {
                damagableTarget.TakeDamage(m_aoeDamage);
            }
        }
    }


}
