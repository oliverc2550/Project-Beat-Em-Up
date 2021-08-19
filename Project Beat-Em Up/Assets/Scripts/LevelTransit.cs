using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Changelog
/*Inital Script created by Oliver (14/07/21)
 */
public class LevelTransit : MonoBehaviour
{
    //OnTriggerEnter changes the scene only when the player enters the trigger volume
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bl_SceneLoader.GetActiveLoader().LoadLevel("EndScene");
        }
    }
}
