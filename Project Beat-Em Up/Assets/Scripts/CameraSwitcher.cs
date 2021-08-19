using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Changelog
/* Inital Script created by Oliver (17/08/21)
 * 17/08/2021 - Thea - Added in additional coroutine and Unity Event to control enemy attack during camera switch. Moved inital corutine out of the update function.
 */

public class CameraSwitcher : MonoBehaviour
{
    #region Variables
    //Seralized Variables for the designer to edit in the inspector
    [SerializeField] protected Cinemachine.CinemachineVirtualCameraBase m_mainCamera;
    [SerializeField] protected Cinemachine.CinemachineVirtualCameraBase m_bossCamera;
    [SerializeField] protected PlayerController m_playerController;
    [SerializeField] [Range(1f, 5f)] private float m_disableCamDelay = 3f;
    protected bool m_bossCameraEnabled;
    protected bool m_hasActivated;
    public UnityEvent onCameraSwitched;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        m_bossCameraEnabled = false;
        m_playerController.m_isBossCamEnabled = false;
        m_hasActivated = false;
    }
    //OnTriggerEnter to activate camera switch, only if the player enters the volume and the switch hasn't been activated already
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && m_hasActivated == false)
        {
            m_bossCameraEnabled = true;
            m_playerController.m_isBossCamEnabled = true;

            StartCoroutine(TutorialAttackWaitTime());
            EnableBossCam();
            StartCoroutine(DisableAfterDelay());
            m_hasActivated = true;
        }
    }
    //Cinemachine Camera Priority changing
    private void EnableBossCam()
    {
        m_bossCamera.Priority = 20;
    }

    private void DisableBossCam()
    {
        m_bossCamera.Priority = 0;
    }
    //Coroutine for enemy attack during camera switch
    IEnumerator TutorialAttackWaitTime()
    {
        Debug.Log("CS");
        yield return new WaitForSeconds(2);
        onCameraSwitched.Invoke();
    }
    //Coroutine to ensure that camera priority is changed back after delay and to ensure that camera switch can't be activated again
    IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(m_disableCamDelay);
        DisableBossCam();
        m_bossCameraEnabled = false;
        m_playerController.m_isBossCamEnabled = false;
    }
}
