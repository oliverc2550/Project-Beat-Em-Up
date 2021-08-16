using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpikesTimer : MonoBehaviour, IDamagable
{
    [SerializeField] private TextMeshProUGUI m_timerText;
    [SerializeField] private int m_maxSeconds;
    [SerializeField] private float m_throwDuration = 0.5f;
    [SerializeField] private GameObject m_spikesPrefab;
    [SerializeField] private float m_minZposForSpikesTimer;
    [SerializeField] private float m_maxZposForSpikesTimer;
    [SerializeField] private float m_minXposForSpikesTimer;
    [SerializeField] private float m_maxXposForSpikesTimer;
    [SerializeField] private float m_spikesYPosition;

    public Lv1Boss enemyBoss;

    public float ImaxHealth { get; set; }
    public float IcurrentHealth { get; set; }
    public bool IisBlocking { get; set; }
    public bool Iinvulnerable { get; set; }

    void Start()
    {
        StartCoroutine(Timer());

        ImaxHealth = 1;
        IcurrentHealth = ImaxHealth;
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
        float zPos = Random.Range(m_minZposForSpikesTimer, m_maxZposForSpikesTimer);
        float xPos = Random.Range(m_minXposForSpikesTimer, m_maxXposForSpikesTimer);

        transform.DOJump(new Vector3(transform.position.x  + xPos + direction.x, transform.position.y + 0.5f, zPos), 1, 1, m_throwDuration);
    }

    private void OnTimerComplete()
    {
        enemyBoss.aliveTimers.Remove(this);

        if (enemyBoss.aliveTimers.Count <= 0)
        {
            GameObject spikes = Instantiate(m_spikesPrefab, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity);
            spikes.transform.DOMoveY(m_spikesYPosition, 0.5f).onComplete = () =>
            {
                spikes.transform.DOMoveY(-2.5f, 0.5f).onComplete = () => { Destroy(spikes); };
            };
        }
        Destroy(gameObject);

    }

    public void OnTakeDamage(float amount)
    {
        IcurrentHealth -= amount;

        if (IcurrentHealth <= 0)
        {
            IcurrentHealth = 0;
            Die();
        };
    }

    public void Die()
    {
        enemyBoss.aliveTimers.Remove(this);

        Destroy(gameObject);
    }
}
