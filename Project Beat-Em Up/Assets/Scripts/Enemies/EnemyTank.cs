using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changelog
/*Script created by Thea
 * 19.08.2021 - Oliver - Added sound
 */

public class EnemyTank : Enemy
{
    #region Variables
    [Header("Tank Settings")]
    [SerializeField] [Range(0, 100)] private int m_chanceToBlock = 100;
    [SerializeField] float m_blockDuration = 1;
    float m_lastMovementSpeed;
    [SerializeField] SphereCollider m_rangeCollider;
    #endregion

    #region Combat
    protected override void Start()
    {
        //  FindObjectOfType<PlayerController>().onNormalAttackEvent.AddListener();
        base.Start();
        GetComponentInChildren<ObjectToDealDamageOnTrigger>().damageToDeal = m_normalAttackDamage;
        m_attackAnimation = "LeftAttack";
        m_lastMovementSpeed = m_movementSpeed;
    }

    // This is called when this enemy takes damage. It is stunned by a chance.
    public override void OnTakeDamage(float damage)
    {
        int chance = 50;

        if (Random.value < chance)
        {
            base.OnTakeDamage(damage);
        }
    }

    // This is called when the player enters the attack range of this enemy. It plays left or right animation based on the direction of the enemy.
    // This animation enables a projectile that damages the player when collided.
    protected override void OnPlayerInRange()
    {
        // if the direction is left
        if (m_spriteRenderer.flipX)
        {
            m_attackAnimation = "LeftAttack";
        }
        else
        {
            m_attackAnimation = "RightAttack";
        }

        m_animator.SetTrigger(m_attackAnimation);
    }

    //Animation event, called when tank attacks
    protected void PlaySlamSound()
    {
        AudioManager.Instance.Play("TankSlamAttackSFX");
    }
    #endregion

    #region Block Events
    // Start listening to events.
    private void OnEnable()
    {
        FindObjectOfType<PlayerController>().onNormalAttackEvent.AddListener(BlockByChance);
        FindObjectOfType<CameraSwitcher>().onCameraSwitched.AddListener(PlayAttackForTutorial);

    }

    // Stop listening to events.
    private void OnDisable()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        CameraSwitcher cameraSwitcher = FindObjectOfType<CameraSwitcher>();

        if (playerController != null)
        {
            playerController.onNormalAttackEvent.RemoveListener(BlockByChance);
        }
        if (cameraSwitcher != null)
        {
            cameraSwitcher.onCameraSwitched.RemoveListener(PlayAttackForTutorial);
        }
    }

    // This is called every time the player attacks by the player's onNormalAttack event. It starts playing the block animation by a chance.
    void BlockByChance()
    {
        float blockChance = (float)m_chanceToBlock / 100;

        if (Random.value < blockChance)
        {
            m_animator.SetBool("isBlocking", true);
            m_movementSpeed = 0;
            IisBlocking = true;

            StartCoroutine(OnBlocking());
        }
    }

    // This coroutine stops playing the block animation after waiting for the block duration.
    IEnumerator OnBlocking()
    {
        yield return new WaitForSeconds(m_blockDuration);

        IisBlocking = false;
        m_animator.SetBool("isBlocking", false);
        m_movementSpeed = m_lastMovementSpeed;
    }
    #endregion

    #region Tutorial
    // Plays the attack animation which is called during tutorial.
    void PlayAttackForTutorial()
    {
        m_animator.SetTrigger(m_attackAnimation);
    }
    #endregion

}
