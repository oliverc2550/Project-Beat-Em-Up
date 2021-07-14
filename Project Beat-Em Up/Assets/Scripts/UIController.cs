using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Changelog
/*Inital Script created by Oliver (09/07/21)
 * SetPercentFill() code from https://youtu.be/oLT4k-lrnwg?t=1065 timestamp ~17:45
 */

public class UIController : MonoBehaviour
{
    [SerializeField] protected Image playerHealthBarBackground;
    [SerializeField] protected Image playerHealthBarForeground;
    [SerializeField] protected Image playerChargeMeterBackground;
    [SerializeField] protected Image playerChargeMeterForeground;
    [SerializeField] protected Image enemyHealthBarBackground;
    [SerializeField] protected Image enemyHealthBarForeground;

    private void SetPercentFill(Image background, Image foreground, float percent)
    {
        float backgroundWidth = background.GetComponent<RectTransform>().rect.width;
        float width = backgroundWidth * percent;
        foreground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    public void SetHealthBarPercent(string targetTag, float percent)
    {
        if(targetTag == "Enemy")
        {
            SetPercentFill(enemyHealthBarBackground, enemyHealthBarForeground, percent);
            //Get Enemy TextUI -> Set it to string name of enemy
        }
        if(targetTag == "Player")
        {
            SetPercentFill(playerHealthBarBackground, playerHealthBarForeground, percent);
        }
    }

    public void SetChargeMeterPercent(float percent)
    {
        SetPercentFill(playerChargeMeterBackground, playerChargeMeterForeground, percent);
    }
}
