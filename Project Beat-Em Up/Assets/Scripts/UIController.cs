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
    public Text ObjectiveDescription;
    public Text FireModeDescription;
    public GameObject PopupBox;
    public Text PopupText;
    public GameObject PauseMenu;
    public GameObject RespawnMenu;
    [HideInInspector] public bool PauseMenuActive;
    [SerializeField] private GameObject _pickupNotification;
    private bool _respawnMenuActive;

    [SerializeField] private TextMeshProUGUI m_scoreText;

    // Start is called before the first frame update
    void Start()
    {
        PauseMenuActive = false; //Sets the PauseMenuActive bool to false
        PopupBox.SetActive(false); //Sets the PopupBox to inactive
        _respawnMenuActive = false; //Sets the respawnMenuActive bool to false
    }
    //Update used to check for key input and to check player progress
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Return) && PopupBox.activeSelf == true)
        //{
        //    PopupBox.SetActive(false);
        //}
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

    //Method to Update the text within the ObjectiveText UI Object
    public void UpdateObjectiveText(string currentObjective)
    {
        ObjectiveDescription.text = currentObjective;
        ObjectiveDescription.color = Color.white;
    }
    //Method to Update the text within the UpdateFireModeText UI Object
    public void UpdateFireModeText(string currentFiremode)
    {
        FireModeDescription.text = currentFiremode;
        FireModeDescription.color = Color.white;
    }
    //Method to Update the text within the UpdatePopupText UI Object
    public void UpdatePopupText(string popupMessage)
    {
        PopupText.text = popupMessage;
        PopupText.color = Color.white;
        if (_pickupNotification.activeSelf == true) //Check to see if the pickupNotification UI Object is activated
        {
            _pickupNotification.SetActive(false); //Deactivate it if it is active so that it doesn't overlap with the popup box
        }
        PopupBox.SetActive(true); //Activates the popup box
    }
    //Method to enable the Pause menu
    public void OnPause(InputAction.CallbackContext value)
    {
        if (value.started && PauseMenu.activeSelf != true && _respawnMenuActive != true) //Check Key input and to see if the pause menu is already active and if the respawn menu is active
        {
            m_playerInput.DeactivateInput();
            if (PopupBox.activeSelf == true) //Check to see if the Popup box is active
            {
                PopupBox.SetActive(false); //Deactivate it if it is
            }
            PauseMenu.SetActive(true); //Set the pause menu to active
            PauseMenuActive = true; //Sets the PauseMenuActive bool to true
            Time.timeScale = 0f; //Pause the game time
            Cursor.visible = true; //Enable the cusor so that the player can interact with the menu
            Cursor.lockState = CursorLockMode.None;
        }
        else if (value.started && PauseMenu.activeSelf == true && _respawnMenuActive != true) //Check Key input and to see if the pause menu is already active and if the respawn menu is active
        {
            Resume(); //Resume the game
        }
    }
    //Method to enable the Respawn menu
    public void EnableRespawnMenu()
    {
        _respawnMenuActive = true; //Sets the respawnMenuActive bool to true
        if (PopupBox.activeSelf == true) //Check to see if the Popup box is active
        {
            PopupBox.SetActive(false);  //Deactivate it if it is
        }
        RespawnMenu.SetActive(true); //Sets the respawn menu to active
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
