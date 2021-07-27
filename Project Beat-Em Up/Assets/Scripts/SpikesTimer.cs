using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpikesTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_timerText;
    [SerializeField] private int m_maxSeconds;
    [SerializeField] private float m_horizontalThrowForce;
    [SerializeField] private float m_verticalThrowForce;
    [SerializeField] private GameObject m_spikesPrefab;

    public List<SpikesTimer> allTimers;

    [SerializeField] private Rigidbody rb;

    void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        int remainingSeconds = m_maxSeconds;

        while (remainingSeconds > 0)
        {
            remainingSeconds--;

            m_timerText.text = remainingSeconds.ToString();

            yield return new WaitForSeconds(1);
        }

        OnTimerComplete();
    }

    public void ThrowSpikesTimer(Vector3 direction)
    {
        rb.AddForce(direction*m_horizontalThrowForce + Vector3.up*m_verticalThrowForce, ForceMode.Impulse);
    }

    private void OnTimerComplete()
    {
        allTimers.Remove(this);

        if (allTimers.Count == 0)
        {
            GameObject spikes = Instantiate(m_spikesPrefab, transform.position, Quaternion.identity);
        }
    }
}
