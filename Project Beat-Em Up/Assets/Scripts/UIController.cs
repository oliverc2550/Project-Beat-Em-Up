using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Changelog
/*Inital Script created by Oliver for use on Module 4 Project "Surviving the Edge"
 * SetPercentFill() code from https://youtu.be/oLT4k-lrnwg?t=1065 timestamp ~17:45
 * 14/07/21 Thea - added score text and changed health bar types from protected to private.
 * commented enemyHealthBar images as there are more than one enemyHealthBar images in the scene and they are independent. Enemy UI logic has been created in EnemyUI class.
 * 15/07/21 - Oliver - Added in ChargeMeter functionality.
 * 18/08/21 - Oliver - Reworked PauseMenu functionality to work with new Unity Input system. Reworked RespawnMenu functionality to RestartMenu as there are no "respawns". 
 * Added in TutorialMenu functionality and removed PopupBox. Removed CurrentObjective and CurrentFiremode functionality and added in Enable/DisablePowerUpDisplay() functions.
 * Added in functionality to detect if a controller is plugged in
 */

public class UIController : MonoBehaviour
{
    #region Variables
    //variables serialized so that they can be set in the editor
    [SerializeField] protected PlayerInput m_playerInput;
    [SerializeField] private Image playerHealthBarBackground;
    [SerializeField] private Image playerHealthBarForeground;
    [SerializeField] private Image playerChargeMeterBackground;
    [SerializeField] private Image playerChargeMeterForeground;
    [SerializeField] private GameObject PowerupDisplay;
    public GameObject PauseMenu;
    [SerializeField] private Button m_resumeButton;
    public GameObject RestartMenu;
    [SerializeField] private Button m_restartButton;
    [SerializeField] private GameObject m_tutorialMenu;
    [SerializeField] private GameObject m_keyboardControlsText;
    [SerializeField] private GameObject m_controllerControlsText;
    [HideInInspector] public bool PauseMenuActive;
    private bool _restartMenuActive;
    [SerializeField] private Text m_livesDisplayText;
    [SerializeField] private TextMeshProUGUI m_scoreText;

    // Start is called before the first frame update
    void Start()
    {
        PauseMenuActive = false; //Sets the PauseMenuActive bool to false
        _restartMenuActive = false; //Sets the respawnMenuActive bool to false
    }
    //Update used to check if a controller is connected and change tutorial message depending
    private void Update()
    {
        if(m_tutorialMenu.activeSelf == true)
        {
            if (Gamepad.current == null)
            {
                m_controllerControlsText.SetActive(false);
                m_keyboardControlsText.SetActive(true);
                //Debug.Log("Gamepad not conected");
            }
            else
            {
                m_keyboardControlsText.SetActive(false);
                m_controllerControlsText.SetActive(true);
            }
        }
    }
    //SetPercentageFunctions used to Update Player Health and Charge bars
    private void SetPercentFill(Image background, Image foreground, float percent)
    {
        float backgroundWidth = background.GetComponent<RectTransform>().rect.width;
        float width = backgroundWidth * percent;
        foreground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    public void SetPlayerHealthBarPercent(float percent)
    {
        SetPercentFill(playerHealthBarBackground, playerHealthBarForeground, percent);
    }

    public void SetChargeMeterPercent(float percent)
    {
        SetPercentFill(playerChargeMeterBackground, playerChargeMeterForeground, percent);
    }
    //Updates Score UI
    public void SetScore(int amount)
    {
        m_scoreText.text = "Score: " + amount.ToString();
    }
    //Updates Lives UI
    public void UpdateLives(int numOfLives)
    {
        m_livesDisplayText.color = Color.white;
        m_livesDisplayText.text = "Lives: " + numOfLives.ToString();
    }

    //Enables/Disables the Powerup Display
    public void EnablePowerUpDisplay()
    {
        PowerupDisplay.SetActive(true);
    }
    public void DisablePowerUpDisplay()
    {
        PowerupDisplay.SetActive(false);
    }
    //Method to enable the Pause menu
    public void OnPause(InputAction.CallbackContext value)
    {
        if (value.performed && PauseMenu.activeSelf != true && _restartMenuActive != true) //Check Key input and to see if the pause menu is already active and if the respawn menu is active
        {
            m_playerInput.DeactivateInput();
            if (m_tutorialMenu.activeSelf == true) //Check to see if the Popup box is active
            {
                m_tutorialMenu.SetActive(false); //Deactivate it if it is
            }
            PauseMenu.SetActive(true); //Set the pause menu to active
            PauseMenuActive = true; //Sets the PauseMenuActive bool to true
            m_resumeButton.Select();
            Time.timeScale = 0f; //Pause the game time
            Cursor.visible = true; //Enable the cusor so that the player can interact with the menu
            Cursor.lockState = CursorLockMode.None;
        }
        else if (value.performed && PauseMenu.activeSelf == true && _restartMenuActive != true) //Check Key input and to see if the pause menu is already active and if the respawn menu is active
        {
            Resume(); //Resume the game
        }
    }
    //Method to enable the Restart menu
    public void EnableRestartMenu()
    {
        _restartMenuActive = true; //Sets the respawnMenuActive bool to true
        m_playerInput.DeactivateInput();
        if (m_tutorialMenu.activeSelf == true) //Check to see if the Popup box is active
        {
            m_tutorialMenu.SetActive(false);  //Deactivate it if it is
        }
        RestartMenu.SetActive(true); //Sets the respawn menu to active
        m_restartButton.Select();
        Time.timeScale = 0f; //Pause the game time
        Cursor.visible = true; //Enable the cusor so that the player can interact with the menu
        Cursor.lockState = CursorLockMode.None;
    }
    //Method to resume the game from the pause menu
    public void Resume()
    {
        m_playerInput.ActivateInput();
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenuActive = false; //Sets the PauseMenuActive bool to false
    }
    //Method to Restart the game after lossing all lives
    public void RestartLevel()
    {
        m_playerInput.ActivateInput();
        bl_SceneLoader.GetActiveLoader().LoadLevel("Level1");
    }
    //Method to return to the main menu
    public void ReturnToMenu()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        m_playerInput.ActivateInput();
        bl_SceneLoader.GetActiveLoader().LoadLevel("MainMenu");
    }
    //Method to quit out of the game
    public void QuitGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
