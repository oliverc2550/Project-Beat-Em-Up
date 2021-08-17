using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : Enemy
{
    [Header("Tank Settings")]
    [SerializeField] [Range(0, 100)] private int m_chanceToBlock = 100;
    [SerializeField] float m_blockDuration = 1;
    float m_lastMovementSpeed;
    protected override void Start()
    {
      //  FindObjectOfType<PlayerController>().onNormalAttackEvent.AddListener();
        base.Start();
        GetComponentInChildren<ObjectToDealDamageOnTrigger>().damageToDeal = m_normalAttackDamage;
        m_attackAnimation = "LeftAttack";
        m_lastMovementSpeed = m_movementSpeed;
    }

    public override void OnTakeDamage(float damage)
    {
        int chance = 50;

        if (Random.value < chance)
        {
            base.OnTakeDamage(damage);
        }

    }

    private void OnEnable()
    {
        FindObjectOfType<PlayerController>().onNormalAttackEvent.AddListener(BlockByChance);
    }

    private void OnDisable()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();

        if (playerController != null)
        {
            playerController.onNormalAttackEvent.RemoveListener(BlockByChance);
        }

    }

    void BlockByChance()
    {
        float blockChance = (float)m_chanceToBlock / 100;

        if (Random.value < blockChance)
        {
            Debug.Log("Blocking");
            m_animator.SetBool("isBlocking", true);
            m_movementSpeed = 0;
            IisBlocking = true;

            StartCoroutine(OnBlocking());
        }
    }

    IEnumerator OnBlocking()
    {
        yield return new WaitForSeconds(m_blockDuration);

        Debug.Log("NOT Blocking");
        IisBlocking = false;
        m_animator.SetBool("isBlocking", false);
        m_movementSpeed = m_lastMovementSpeed;
    }

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


}
