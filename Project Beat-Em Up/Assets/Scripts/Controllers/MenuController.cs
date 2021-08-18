using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    //Method to Delete all PlayerPref Save Data
    public void DeleteSaveData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Save Data Deleted");
    }

    public void Update()
    {
        if(Gamepad.current == null)
        {
            m_controllerNotification.SetActive(true);
            //Debug.Log("Gamepad not conected");
        }
        else
        {
            m_controllerNotification.SetActive(false);
        }
    }
}
