﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changelog
/*Inital Script created by Oliver (13/07/21)
 */

public enum PickupItems { HealthGain, ChargeGain, ScoreIncrease, Invulnerability }
public class PickupsController : MonoBehaviour
{
    private PickupItems m_pickupItems;
    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;

    [Header("References (DO NOT EDIT)")]
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] private UIController m_UIController;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] private Material m_healthPickupMat;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] private Material m_chargePickupMat;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] private Material m_scorePickupMat;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] private Material m_invulnerabilityMat;

    [Header("Settings")]
    [Range(15, 65)] public float m_healthPickupAmout;
    [Range(5, 45)] public float m_chargePickupAmout;
    [Range(10, 150)] public int m_scoreGainedOnItemPicked;

    private PickupItems GetRandomItem()
    {
        m_pickupItems = (PickupItems)UnityEngine.Random.Range(0, Enum.GetNames(typeof(PickupItems)).Length);
        return m_pickupItems;
    }

    private void SetItem()
    {
        switch (m_pickupItems)
        {
            case PickupItems.HealthGain:
                m_spriteRenderer.material = m_healthPickupMat;
                m_animator.SetBool("Health", true);
                break;

            case PickupItems.ChargeGain:
                m_spriteRenderer.material = m_chargePickupMat;
                m_animator.SetBool("Charge", true);
                break;

            case PickupItems.ScoreIncrease:
                m_spriteRenderer.material = m_scorePickupMat;
                m_animator.SetBool("Score", true);
                break;

            case PickupItems.Invulnerability:
                m_spriteRenderer.material = m_invulnerabilityMat;
                m_animator.SetBool("Invulnerable", true);
                break;
        }
    }

    public void PickupEffects(GameObject gameObject)
    {
        switch (m_pickupItems)
        {
            case PickupItems.HealthGain:
                gameObject.GetComponent<IDamagable>().IcurrentHealth += m_healthPickupAmout;
                break;

            case PickupItems.ChargeGain:
                gameObject.GetComponent<PlayerController>().currentCharge += m_chargePickupAmout;
                break;

            case PickupItems.ScoreIncrease:
                FindObjectOfType<ScoreManager>().AddScore(m_scoreGainedOnItemPicked);
                Debug.Log("Score increases by " + m_scoreGainedOnItemPicked);
                break;

            case PickupItems.Invulnerability:
                gameObject.GetComponent<CombatandMovement>().m_invulnerable = true;
                m_UIController.EnablePowerUpDisplay();
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
        GetRandomItem();
        Debug.Log(m_pickupItems);
        SetItem();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickupEffects(other.gameObject);
            Destroy(gameObject);
        }
    }
}
