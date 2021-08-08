using System;
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

    [Header("References (DO NOT EDIT)")]
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] private Sprite m_healthPickupSprite;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] private Sprite m_chargePickupSprite;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] private Sprite m_scorePickupSprite;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] private Sprite m_invulnerabilitySprite;
    [SerializeField] int m_scoreGainedOnItemPicked = 50;

    [Header("Settings")]
    [Range(15, 65)] public float m_healthPickupAmout;
    [Range(5, 45)] public float m_chargePickupAmout;
    [Range(10, 150)] public float m_scorePickupAmout;

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
                m_spriteRenderer.sprite = m_healthPickupSprite;
                break;

            case PickupItems.ChargeGain:
                m_spriteRenderer.sprite = m_chargePickupSprite;
                break;

            case PickupItems.ScoreIncrease:
                m_spriteRenderer.sprite = m_scorePickupSprite;
                break;

            case PickupItems.Invulnerability:
                m_spriteRenderer.sprite = m_invulnerabilitySprite;
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
                gameObject.GetComponent<PlayerController>().m_currentCharge += m_chargePickupAmout;
                break;

            case PickupItems.ScoreIncrease:
                FindObjectOfType<ScoreManager>().AddScore(m_scoreGainedOnItemPicked);
                Debug.Log("Score increases by " + m_scorePickupAmout);
                break;

            case PickupItems.Invulnerability:
                gameObject.GetComponent<CombatandMovement>().m_invulnerable = true;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetRandomItem();
        Debug.Log(m_pickupItems);
        SetItem();
    }
}
