using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Changelog
/*Inital Script created by Oliver (09/07/21)
 * SetPercentFill() code from https://youtu.be/oLT4k-lrnwg?t=1065 timestamp ~17:45
 * 14/07/21 Thea - added score text and changed health bar types from protected to private.
 * commented enemyHealthBar images as there are more than one enemyHealthBar images in the scene and they are independent. Enemy UI logic has been created in EnemyUI class.
 */

public class UIController : MonoBehaviour
{
    [SerializeField] private Image playerHealthBarBackground;
    [SerializeField] private Image playerHealthBarForeground;
    [SerializeField] private Image playerChargeMeterBackground;
    [SerializeField] private Image playerChargeMeterForeground;
    //[SerializeField] private Image enemyHealthBarBackground;
    //[SerializeField] private Image enemyHealthBarForeground;

    [SerializeField] private TextMeshProUGUI m_scoreText;

    private void SetPercentFill(Image background, Image foreground, float percent)
    {
        float backgroundWidth = background.GetComponent<RectTransform>().rect.width;
        float width = backgroundWidth * percent;
        foreground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    public void SetPlayerHealthBarPercent(float percent)
    {
        //if(targetTag == "Enemy")
        //{
        //    SetPercentFill(enemyHealthBarBackground, enemyHealthBarForeground, percent);
        //    //Get Enemy TextUI -> Set it to string name of enemy
        //}
        //if (targetTag == "Player")
        //{
        //}
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
}
