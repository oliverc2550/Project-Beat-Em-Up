using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankProjectile : MonoBehaviour
{
    [SerializeField] int m_damageToDeal = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamagable>().TakeDamage(m_damageToDeal);
            Debug.Log(other.name + ": " + other.GetComponent<IDamagable>().IcurrentHealth);
        }
    }

}

