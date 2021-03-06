using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changelog
/*Inital Script created by Thea
 */

public class EnemyThief : Enemy
{
    #region Variables
    [Header("Thief Settings")]
    [SerializeField][Range(0,50)] private float m_ChargeToStealOnHit = 10;
    [SerializeField][Range(0,100)] private float m_ChanceToStealCharge = 30;
    [SerializeField] private float m_healthTresholdToRun = 20;
    PlayerController m_playerController;
    private bool m_chargeStollen = false;
    #endregion

    protected override void Start()
    {
        base.Start();
        m_playerController = FindObjectOfType<PlayerController>();
    }

    #region Set states
    protected override void Update()
    {
        base.Update();

        // Checking if the health of this thief is less than x amount. If so, it starts running.
        // If the player has more than 50% charge, the thief starts chasing the player to steal it. Otherwise it patrols.
        if (IcurrentHealth <= m_healthTresholdToRun)
        {
            SetEnemyState(EnemyState.Run);
        }

        if (m_playerController.currentCharge > m_playerController.maxCharge / 2 && m_currentState == EnemyState.Patrol)
        {
            SetEnemyState(EnemyState.Chase);
            m_chargeStollen = false;
            m_alreadyAttackedOnce = false;
        }
    }
    #endregion

    #region Steal charge
    // It is called when the player gets in attack range. The thief steals the charge if its not stollen yet, once a charge is stolen, he returns back to patrolling.
    protected override void OnPlayerInRange()
    {
        if (m_chargeStollen)
        {
            SetEnemyState(EnemyState.Patrol);
        }
        else if (!m_alreadyAttackedOnce)
        {
            base.OnPlayerInRange();
            m_alreadyAttackedOnce = false;
        }
    }

    // Animation event called during attack animation. It steals players charge by a chance.
    protected override void AttackEffects(GameObject gameObject)
    {
        PlayerController player = gameObject.GetComponent<PlayerController>();

        if (Random.value < m_ChanceToStealCharge / 100 && !m_chargeStollen)
        {
            StealCharge(player);
            m_chargeStollen = true;
        }
    }

    // Steals charge from the player.
    private void StealCharge(PlayerController player)
    {
        player.currentCharge -= m_ChargeToStealOnHit;
        if (player.currentCharge < 0)
        {
            player.currentCharge = 0;
        }
        Debug.Log("Player's charge is stolen by " + m_ChargeToStealOnHit + ". New charge is: " + player.currentCharge);
    }
    #endregion
}
