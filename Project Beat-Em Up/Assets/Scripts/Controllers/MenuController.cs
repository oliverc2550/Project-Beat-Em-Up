using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Changelog
/* Inital Script created by Oliver for use on Module 4 Project "Surviving the Edge"
 * 10/08/21 - Oliver - Changed scene loading script to use bl_SceneLoader instead of BuildIndex to work with Loading Scene asset. Removed DeleteSave function as save data isn't used
 * 17/08/21 - Oliver - Added in m_controllerNotification and update function to alert the player the game plays best with a controller
 */
public class MenuController : MonoBehaviour
{
    [SerializeField] protected GameObject m_controllerNotification;

    //Method to load the main game level from the main menu
    public void PlayGame()
    {
        bl_SceneLoader.GetActiveLoader().LoadLevel("Level1");
    }
    //Method to return to the main menu From any scene
    public void ReturnToMenu()
    {
        bl_SceneLoader.GetActiveLoader().LoadLevel("MainMenu");
    }
    //Method to Quit the game
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
    public void Update()
    {
        if(Gamepad.current == null)
        {
            m_controllerNotification.SetActive(true);
        }
        else
        {
            m_controllerNotification.SetActive(false);
        }
    }
}
