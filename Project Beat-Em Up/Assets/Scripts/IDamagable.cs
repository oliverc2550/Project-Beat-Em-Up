using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changelog
/*Inital Script created by Thea (1/07/21)
 */


// https://learn.unity.com/tutorial/interfaces
// https://stackoverflow.com/questions/5698477/can-an-interface-contain-a-variable
// https://www.youtube.com/watch?v=A7qwuFnyIpM
public interface IDamageable
{
    // every class that inherits from this interface is forced to have health, deal damage and die.
    float maxHealth { get; set; }
    float currentHealth { get; set; }
    bool isBlocking { get; set; }

    void OnTakeDamage(float amount);
    void Die();
}

