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
    #region Variables
    //Variables serialized so that the designer could edit them within the editor
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
    #endregion
    //Function to randomly select an enum from the list of pickups and return it
    private PickupItems GetRandomItem()
    {
        m_pickupItems = (PickupItems)UnityEngine.Random.Range(0, Enum.GetNames(typeof(PickupItems)).Length);
        return m_pickupItems;
    }
    //Function to set certian aspects of the pickup depending on which enum was choosen
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
    //Function that causes the pickup to take effect, again based on choosen enum
    public void PickupEffects(GameObject gameObject)
    {
        switch (m_pickupItems)
        {
            case PickupItems.HealthGain:
                gameObject.GetComponent<IDamagable>().IcurrentHealth += m_healthPickupAmout;
                SetToNull(gameObject);
                Destroy(this.gameObject);
                break;

            case PickupItems.ChargeGain:
                gameObject.GetComponent<PlayerController>().currentCharge += m_chargePickupAmout;
                SetToNull(gameObject);
                Destroy(this.gameObject);
                break;

            case PickupItems.ScoreIncrease:
                FindObjectOfType<ScoreManager>().AddScore(m_scoreGainedOnItemPicked);
                SetToNull(gameObject);
                Destroy(this.gameObject);
                break;

            case PickupItems.Invulnerability:
                gameObject.GetComponent<CombatandMovement>().m_invulnerable = true;
                m_UIController.EnablePowerUpDisplay();
                SetToNull(gameObject);
                Destroy(this.gameObject);
                break;
        }
    }
    //Function to reset variables found within the player controller so that the player would be unable to cause errors by trying to reactivate an already recieved pickup
    private void SetToNull(GameObject gameObject)
    {
        gameObject.GetComponent<PlayerController>().m_pickupController = null;
        gameObject.GetComponent<PlayerController>().m_pickupInRange = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
        m_UIController = FindObjectOfType<UIController>();
        GetRandomItem();
        Debug.Log(m_pickupItems);
        SetItem();

    }
    //OnTriggerEnter allows the player to pick up the pickup that they are currently nearby
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.m_pickupController = gameObject.GetComponent<PickupsController>();
            playerController.m_pickupInRange = true;
        }
    }
    //OnTrigerExit insures that the player can not pick up a pickup they are not near
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetToNull(other.gameObject);
        }
    }
}
