using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] protected Cinemachine.CinemachineVirtualCameraBase m_mainCamera;
    [SerializeField] protected Cinemachine.CinemachineVirtualCameraBase m_bossCamera;
    [SerializeField] protected PlayerController m_playerController;
    [SerializeField] [Range(1f, 5f)] private float m_disableCamDelay = 3f;
    protected bool m_bossCameraEnabled;
    protected bool m_hasActivated;

    public UnityEvent onCameraSwitched;

    // Start is called before the first frame update
    void Start()
    {
        m_bossCameraEnabled = false;
        m_playerController.m_isBossCamEnabled = false;
        m_hasActivated = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && m_hasActivated == false)
        {
            m_bossCameraEnabled = true;
            m_playerController.m_isBossCamEnabled = true;

            StartCoroutine(TutorialAttackWaitTime());

            //moved from update
            EnableBossCam();
            StartCoroutine(DisableAfterDelay());
            m_hasActivated = true;
        }
    }

    private void EnableBossCam()
    {
        m_bossCamera.Priority = 20;
    }

    private void DisableBossCam()
    {
        m_bossCamera.Priority = 0;
    }

    IEnumerator TutorialAttackWaitTime()
    {
        Debug.Log("CS");
        yield return new WaitForSeconds(2);
        onCameraSwitched.Invoke();
    }

    IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(m_disableCamDelay);
        DisableBossCam();
        m_bossCameraEnabled = false;
        m_playerController.m_isBossCamEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(m_bossCameraEnabled == true && m_hasActivated == false)
        //{
        //    EnableBossCam();
        //    StartCoroutine(DisableAfterDelay());
        //    m_hasActivated = true;
        //}
    }
}
