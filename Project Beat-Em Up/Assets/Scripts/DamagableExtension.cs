using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changelog
/*Inital Script created by Oliver (04/07/21)
 * 04/07/21 - Oliver - Created DamagableExtension to allow for common implementation code to be added to IDamagable to reduce duplicate code.
 * 13/07/21 - Oliver - Added in Iinvulnerable functionality.
 */

public static class DamagableExtension
{
    public static void TakeDamage(this IDamagable idamagable, float damage)
    {
        if(idamagable.IisBlocking == true)
        {
            idamagable.IcurrentHealth += (damage / 2);
        }
        if (idamagable.Iinvulnerable == true)
        {
            idamagable.IcurrentHealth -= (damage * 0);
        }
        else
        {
            idamagable.IcurrentHealth -= damage;
        }
        //add in health bar updating to individual implementation
        idamagable.OnTakeDamage(damage);
        if (idamagable.IcurrentHealth <= 0)
        {
            idamagable.Die();
        }
    }
}
