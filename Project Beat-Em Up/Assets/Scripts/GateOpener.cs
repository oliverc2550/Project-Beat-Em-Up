using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

//Changelog
/*Inital Script created by Oliver
 * 8.08.2021 - Thea - Shake the camera while the gate is opening
 * 10.08.2021 - Thea - Opening the gate with DoTween and only if all the enemies from the previous combat area are defeated
 * 19.08.2021 - Thea - Closing the gate only if new enemies has spawned and the gate is currently open so that player won't get stuck in the previous combat area
 */

public class GateOpener : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera m_playerCamera;
    [SerializeField] int m_gateMovementDuration = 10;

    public bool m_hasOpened;
    private float m_startingPosZ;
    private float m_endPosZ;

    private void Start()
    {
        m_hasOpened = false;
        m_startingPosZ = transform.position.z;
        m_endPosZ = transform.position.z - 10;

    }

    private void OnTriggerEnter(Collider other)
    {
        int enemyCount = FindObjectOfType<EnemySpawner>().enemyCount;

        if (other.gameObject.CompareTag("Player") && m_hasOpened == false && enemyCount <= 0)
        {
            AudioManager.Instance.Play("GateOpening");
            m_playerCamera.transform.DOShakeRotation(m_gateMovementDuration, 0.35f);
            transform.DOMoveZ(m_endPosZ, m_gateMovementDuration);

            m_hasOpened = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        int enemyCount = FindObjectOfType<EnemySpawner>().enemyCount;

        if (other.gameObject.CompareTag("Player") && m_hasOpened == true && enemyCount > 0)
        {
            transform.DOMoveZ(m_startingPosZ, m_gateMovementDuration);
            m_hasOpened = false;
        }
    }
}
