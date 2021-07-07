using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changelog
/*Inital Script created by Thea (1/07/21)
 * 04/07/21 - Oliver - Changed health to maxHealth and added currentHealth and isBlocking. Changed DealDamage() to OnTakeDamage() for better outside readability.
 * 07/07/21 - Oliver - Added I to the start of all properties to help with readability and recognition that these are part of the interface.
 */


// https://learn.unity.com/tutorial/interfaces
// https://stackoverflow.com/questions/5698477/can-an-interface-contain-a-variable
// https://www.youtube.com/watch?v=A7qwuFnyIpM
public interface IDamagable
{
    // every class that inherits from this interface is forced to have health, deal damage and die.
    float ImaxHealth { get; set; }
    float IcurrentHealth { get; set; }
    bool IisBlocking { get; set; }

    void OnTakeDamage(float amount);
    void Die();

}

