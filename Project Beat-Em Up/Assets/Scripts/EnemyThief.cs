using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Changelog
/*Inital Script created by Thea (6/07/21)
 */
public class EnemyThief : Enemy
{
    [SerializeField] private float m_healthTresholdToRun = 20;
    protected override void Update()
    {
        base.Update();
        Debug.Log(currentHealth);
        Debug.Log(maxHealth);

        if (currentHealth <= m_healthTresholdToRun)
        {
            StartRunning();
        }
    }

    private void StartRunning()
    {
        SetEnemyState(EnemyState.Run);
    }

    protected override Collider[] Attack(Transform attackPoint, float attackRange, LayerMask enemyLayer, float attackDamage)
    {
        Collider[] colliders = base.Attack(attackPoint, attackRange, enemyLayer, attackDamage);
        Debug.Log(colliders.Length);
        if (colliders.Length > 0)
        {
            StealWeapon(colliders[0].GetComponent<CombatandMovement>());
        }

        return colliders;
    }


    private void StealWeapon(CombatandMovement combatAndMovement)
    {
        if (combatAndMovement.heldObject != null)
        {
            EquipItem(combatAndMovement.heldObject);
            combatAndMovement.heldObject = null;
        }
    }
}
