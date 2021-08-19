using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changelog
/*Inital Script created by Thea
 * 18/08/21 - Oliver - Added in functionality to grant the player an addition life after a certian score threshold has been reached
 */
public class ScoreManager : MonoBehaviour
{
    #region Variables
    private int m_score = 0;
    private int m_scoreToLives = 0;
    [SerializeField] private int m_grantLifePerScore = 5000;
    [SerializeField] private UIController m_uiController;
    [SerializeField] private PlayerController m_playerController;
    #endregion

    // This is called when an enemy dies. It adds score to the player and updates the UI text.
    public void AddScore(int amount)
    {
        m_score += amount;
        m_scoreToLives += amount;
        m_uiController.SetScore(m_score);
        if(m_scoreToLives >= m_grantLifePerScore)
        {
            m_playerController.m_playerLives += 1;
            m_uiController.UpdateLives(m_playerController.m_playerLives);
            m_scoreToLives = 0;
        }
    }
}
