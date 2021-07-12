using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Changelog
/*Inital Script created by Thea (7/07/21)
 */
public class EnemySummoner : Enemy
{
    //[Header("Summoner Settings")]

    [SerializeField] private Enemy m_EnemyToSummon;

    Enemy m_SummonedEnemy;

    protected override void Move(Vector3 direction)
    {
        if (!PlayerInRange())
        {
            base.Move(direction);
        }
        else
        {
            m_animator.SetBool("Walking", false);
        }
    }

    protected override void OnPlayerInRange()
    {
        if (m_SummonedEnemy == null)
        {
            m_animator.SetTrigger("Summon");
        }
    }

    // animation event
    private void SummonAnimEvent()
    {
        if (m_SummonedEnemy == null)
        {
            m_SummonedEnemy = Instantiate(m_EnemyToSummon, transform.position, Quaternion.identity);
        }
    }
}
