using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changelog
/*Inital Script created by Thea (01/07/21)
 * 04/07/21 - Oliver - Created instance of IDamagable due to github errors. Changed health to maxHealth and added currentHealth and isBlocking.
 * Changed DealDamage() to OnTakeDamage() for better outside readability.
 */

public interface IDamagable_Oliver
{
    // every class that inherits from this interface is forced to have health, deal damage and die.
    float maxHealth { get; set; }
    float currentHealth { get; set; }
    bool isBlocking { get; set; }

    void OnTakeDamage(float amount);
    void Die();

}
