using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [Tooltip("For best effect keep the X and Y values between 0 and 1.")]
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    private Transform m_cameraTransform;
    private Vector3 m_lastCameraPos;

    // Start is called before the first frame update
    void Start()
    {
        m_cameraTransform = Camera.main.transform;
        m_lastCameraPos = m_cameraTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = m_cameraTransform.position - m_lastCameraPos;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
        m_lastCameraPos = m_cameraTransform.position;
    }
}
