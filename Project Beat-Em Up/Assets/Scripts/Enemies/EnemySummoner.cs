using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changelog
/*Script created by Thea
* 19.08.2021 - Oliver - Added sound
 */

public class EnemySummoner : Enemy
{
    #region Variables

    [SerializeField] private Enemy m_EnemyToSummon;

    Enemy m_SummonedEnemy;
    #endregion

    #region Overriding main functions
    // This overrides the move function in order to stop this enemy from attacking when it gets close to the player.
    // Once this enemy gets in range of the player, it stops moving and attacking.
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

        LookAtDirection(-direction.x);
    }

    // This is called when the player gets in range of this summoner. It plays the summon animation if there is no summoned enemy.
    protected override void OnPlayerInRange()
    {
        if (m_SummonedEnemy == null)
        {
            m_animator.SetTrigger("Summon");
        }
    }
    #endregion

    #region Animation events
    // This is an animation event that is called while playing summon animation. It spawns the summoned enemy if there is none alive.
    private void SummonAnimEvent()
    {
        if (m_SummonedEnemy == null)
        {
            AudioManager.Instance.Play("Summon/StealSFX");
            m_SummonedEnemy = Instantiate(m_EnemyToSummon, transform.position, Quaternion.Euler(0f, 180f, 0f));
            m_enemySpawner.enemyCount++;
        }

    }
    #endregion
}
