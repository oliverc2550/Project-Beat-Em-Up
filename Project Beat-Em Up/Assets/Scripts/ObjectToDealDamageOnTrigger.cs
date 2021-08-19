using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToDealDamageOnTrigger : MonoBehaviour
{
    [HideInInspector] public float damageToDeal;

    // When an IDamageable object triggers with this object, deal damage to it.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Explosive"))
        {
            other.GetComponent<IDamagable>().TakeDamage(damageToDeal);
            Debug.Log(other.name + ": " + other.GetComponent<IDamagable>().IcurrentHealth);
        }
    }

}

