using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

//Changelog
/*Inital Script created by Oliver
 * 9.08.2021 - Thea - Shake the camera while the gate is opening
 */

public class GateOpener : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera m_playerCamera;
    [SerializeField] int m_cameraShakeDuration = 10;

    private bool m_isOpening;
    private bool m_isClosing;
    private bool m_hasOpened;
    private bool m_hasClosed;
    private static Vector3 startingPos;

    private void Start()
    {
        m_isOpening = false;
        m_isClosing = false;
        m_hasOpened = false;
        m_hasClosed = false;
        startingPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && m_hasOpened == false && m_hasOpened == false)
        {
            m_isOpening = true;
            m_hasOpened = true;

            m_playerCamera.transform.DOShakeRotation(m_cameraShakeDuration, 0.3f);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && m_hasOpened == true && m_hasClosed == false)
        {
            m_isClosing = true;
            m_hasClosed = true;
        }
    }

    private void Update()
    {
        Debug.Log("zPos1: " + transform.position.z);
        if (m_isOpening == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - 10f), 1f * Time.deltaTime);

            Debug.Log("zPos2: " + transform.position.z);
        }
        if(m_isClosing == true)
        {
            m_isOpening = false;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y,startingPos.z), 1f * Time.deltaTime);

            Debug.Log("zPos3: " + transform.position.z);
        }
    }
}
