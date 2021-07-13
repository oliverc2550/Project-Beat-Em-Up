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
    protected override void Update()
    {
        base.Update();

        if (IcurrentHealth <= m_healthTresholdToRun)
        {
            StartRunning();
        }
    }

    private void StartRunning()
    {
        SetEnemyState(EnemyState.Run);
    }

    protected override void AttackEffects(GameObject gameObject)
    {
        //Debug.Log(gameObject);
        if (Random.value < m_ChanceToStealCharge/100)
        {
            StealCharge(gameObject.GetComponent<PlayerController>());
        }
    }

    private void StealCharge(PlayerController player)
    {
        player.m_currentCharge -= m_ChargeToStealOnHit;
        if (player.m_currentCharge < 0)
        {
            player.m_currentCharge = 0;
        }
        Debug.Log("Player's charge is stolen by " + m_ChargeToStealOnHit + ". New charge is: " + player.m_currentCharge);
    }
}
