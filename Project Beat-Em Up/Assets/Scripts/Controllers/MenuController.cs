using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
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
}
