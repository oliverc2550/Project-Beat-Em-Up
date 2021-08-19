using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changelog
/*Inital Script created by Oliver (09/07/21)
 * ParallaxController code from https://youtu.be/wBol2xzxCOU?t=195 timestamp ~3:15
 */

public class ParallaxController : MonoBehaviour
{
    #region Variables
    //m_parallaxEffectMultiplier serialized so that values could be edited by our designer
    [Tooltip("For best effect keep the X and Y values between 0 and 1.")]
    [SerializeField] private Vector2 m_parallaxEffectMultiplier;
    private Transform m_cameraTransform;
    private Vector3 m_lastCameraPos;
    #endregion

    // Setting camera positions at the start of the script
    void Start()
    {
        m_cameraTransform = Camera.main.transform;
        m_lastCameraPos = m_cameraTransform.position;
    }
    //Adjusting elements that are parallaxing based on the camera movement. Done during LateUpdate to ensure that it happens after the camera moves
    void LateUpdate()
    {
        Vector3 deltaMovement = m_cameraTransform.position - m_lastCameraPos;
        transform.position += new Vector3(deltaMovement.x * m_parallaxEffectMultiplier.x, deltaMovement.y * m_parallaxEffectMultiplier.y);
        m_lastCameraPos = m_cameraTransform.position;
    }
}
