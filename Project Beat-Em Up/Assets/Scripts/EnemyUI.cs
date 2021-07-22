using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 5f)] private float hideDelay = 1f;
    [SerializeField] private GameObject m_enemyUI;
    [SerializeField] public Image fillImage;
    [SerializeField] private TextMeshProUGUI m_enemyName;

    private IEnumerator m_hideCoroutine = null;

    private void Start()
    {
        m_enemyUI.SetActive(false);
    }
    public void SetEnemyNameUI(string name)
    {
        m_enemyName.text = name;
    }
    public void SetHealthUI(float health, float maxHealth)
    {
        fillImage.fillAmount = health / maxHealth;

        m_enemyUI.SetActive(true);

        if (m_hideCoroutine == null)
        {
            m_hideCoroutine = HideAfterDelay();
            StartCoroutine(m_hideCoroutine);
        }
    }


    // how to start a coroutine only once: https://stackoverflow.com/questions/43461214/how-can-i-run-startcoroutine-once
    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(hideDelay);
        m_enemyUI.SetActive(false);
        m_hideCoroutine = null;
    }
}
