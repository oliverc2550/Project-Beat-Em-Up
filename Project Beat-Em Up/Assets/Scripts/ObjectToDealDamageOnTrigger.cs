using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToDealDamageOnTrigger : MonoBehaviour
{
    public float damageToDeal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamagable>().TakeDamage(damageToDeal);
            Debug.Log(other.name + ": " + other.GetComponent<IDamagable>().IcurrentHealth);
        }
    }

}

