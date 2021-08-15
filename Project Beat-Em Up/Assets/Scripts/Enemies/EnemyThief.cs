using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Changelog
/*Inital Script created by Thea (6/07/21)
 */
public class EnemyThief : Enemy
{
    [Header("Thief Settings")]
    [SerializeField][Range(0,50)] private float m_ChargeToStealOnHit = 10;
    [SerializeField][Range(0,100)] private float m_ChanceToStealCharge = 30;
    [SerializeField] private float m_healthTresholdToRun = 20;
    PlayerController m_playerController;
    private bool m_chargeStollen = false;


    protected override void Start()
    {
        base.Start();
        m_playerController = FindObjectOfType<PlayerController>();
    }

    protected override void Update()
    {
        base.Update();

        if (IcurrentHealth <= m_healthTresholdToRun)
        {
            SetEnemyState(EnemyState.Run);
        }
        if (m_playerController.currentCharge > m_playerController.maxCharge/2 && m_currentState == EnemyState.Patrol)
        {
            SetEnemyState(EnemyState.Chase);
            m_chargeStollen = false;
            m_alreadyAttackedOnce = false;
        }
    }

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

    //anim event
    protected override void AttackEffects(GameObject gameObject)
    {
        PlayerController player = gameObject.GetComponent<PlayerController>();

        if (Random.value < m_ChanceToStealCharge / 100 && !m_chargeStollen)
        {
            StealCharge(player);
            m_chargeStollen = true;
        }
    }

    private void StealCharge(PlayerController player)
    {
        player.currentCharge -= m_ChargeToStealOnHit;
        if (player.currentCharge < 0)
        {
            player.currentCharge = 0;
        }
        Debug.Log("Player's charge is stolen by " + m_ChargeToStealOnHit + ". New charge is: " + player.currentCharge);
    }
}
