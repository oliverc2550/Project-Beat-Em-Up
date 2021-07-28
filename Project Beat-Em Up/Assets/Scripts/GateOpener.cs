using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpener : MonoBehaviour
{
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
        if(m_hasOpened == false && m_hasOpened == false)
        {
            m_isOpening = true;
            m_hasOpened = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(m_hasOpened == true && m_hasClosed == false)
        {
            m_isClosing = true;
            m_hasClosed = true;
        }
    }

    private void Update()
    {
        if(m_isOpening == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(startingPos.x, startingPos.y, startingPos.z - 10f), 1f * Time.deltaTime);
        }
        if(m_isClosing == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPos, 1f * Time.deltaTime);
        }
    }
}
