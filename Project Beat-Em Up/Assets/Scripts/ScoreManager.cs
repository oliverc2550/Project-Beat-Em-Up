using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changelog
/*Inital Script created by Thea
 */
public class ScoreManager : MonoBehaviour
{
    private int m_score = 0;
    [SerializeField] private UIController m_uiController;

    public void AddScore(int amount)
    {
        m_score += amount;
        m_uiController.SetScore(m_score);
    }
}
