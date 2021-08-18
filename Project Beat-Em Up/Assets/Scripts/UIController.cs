using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Changelog
/*Inital Script created by Oliver (09/07/21)
 * SetPercentFill() code from https://youtu.be/oLT4k-lrnwg?t=1065 timestamp ~17:45
 * 14/07/21 Thea - added score text and changed health bar types from protected to private.
 * commented enemyHealthBar images as there are more than one enemyHealthBar images in the scene and they are independent. Enemy UI logic has been created in EnemyUI class.
 */

public class UIController : MonoBehaviour
{
    [SerializeField] protected PlayerInput m_playerInput;
    [SerializeField] private Image playerHealthBarBackground;
    [SerializeField] private Image playerHealthBarForeground;
    [SerializeField] private Image playerChargeMeterBackground;
    [SerializeField] private Image playerChargeMeterForeground;
    [SerializeField] private GameObject PowerupDisplay;
    public GameObject PopupBox;
    public Text PopupText;
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
        PopupBox.SetActive(false); //Sets the PopupBox to inactive
        _restartMenuActive = false; //Sets the respawnMenuActive bool to false
    }
    //Update used to check for key input and to check player progress
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

    public void SetScore(int amount)
    {
        m_scoreText.text = "Score: " + amount.ToString();
    }

    public void UpdateLives(int numOfLives)
    {
        m_livesDisplayText.color = Color.white;
        m_livesDisplayText.text = "Lives: " + numOfLives.ToString();
    }

    //Method to Update the text within the UpdateFireModeText UI Object
    public void EnablePowerUpDisplay()
    {
        PowerupDisplay.SetActive(true);
    }
    public void DisablePowerUpDisplay()
    {
        PowerupDisplay.SetActive(false);
    }
    //Method to Update the text within the UpdatePopupText UI Object
    public void UpdatePopupText(string popupMessage)
    {
        PopupText.text = popupMessage;
        PopupText.color = Color.white;
        PopupBox.SetActive(true); //Activates the popup box
    }
    //Method to enable the Pause menu
    public void OnPause(InputAction.CallbackContext value)
    {
        if (value.started && PauseMenu.activeSelf != true && _restartMenuActive != true) //Check Key input and to see if the pause menu is already active and if the respawn menu is active
        {
            m_playerInput.DeactivateInput();
            if (PopupBox.activeSelf == true) //Check to see if the Popup box is active
            {
                PopupBox.SetActive(false); //Deactivate it if it is
            }
            PauseMenu.SetActive(true); //Set the pause menu to active
            PauseMenuActive = true; //Sets the PauseMenuActive bool to true
            m_resumeButton.Select();
            Time.timeScale = 0f; //Pause the game time
            Cursor.visible = true; //Enable the cusor so that the player can interact with the menu
            Cursor.lockState = CursorLockMode.None;
        }
        else if (value.started && PauseMenu.activeSelf == true && _restartMenuActive != true) //Check Key input and to see if the pause menu is already active and if the respawn menu is active
        {
            Resume(); //Resume the game
        }
    }
    //Method to enable the Respawn menu
    public void EnableRestartMenu()
    {
        _restartMenuActive = true; //Sets the respawnMenuActive bool to true
        m_playerInput.DeactivateInput();
        if (PopupBox.activeSelf == true) //Check to see if the Popup box is active
        {
            PopupBox.SetActive(false);  //Deactivate it if it is
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
    //Method to Respawn after dying
    public void RestartLevel()
    {
        m_playerInput.ActivateInput();
        bl_SceneLoader.GetActiveLoader().LoadLevel("Level1");
        //RespawnMenu.SetActive(false);
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        //_respawnMenuActive = false;
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
