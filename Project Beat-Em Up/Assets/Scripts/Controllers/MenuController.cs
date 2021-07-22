using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //Method to load the main game level from the main menu
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //Method to return to the main menu From any scene
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
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
