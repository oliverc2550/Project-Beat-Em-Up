using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Changelog
/*Script created by Thea
  */

public class ObjectToDealDamageOnTrigger : MonoBehaviour
{
    #region Variables
    [HideInInspector] public float damageToDeal;

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        // When an IDamageable object triggers with this object, deal damage to it.

        if (other.CompareTag("Player") || other.CompareTag("Explosive"))
        {
            other.GetComponent<IDamagable>().TakeDamage(damageToDeal);

            Debug.Log(other.name + ": " + other.GetComponent<IDamagable>().IcurrentHealth);
        }
    }

}

