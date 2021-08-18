using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour, IDamagable
{
    [SerializeField] protected GameObject m_explosionVFX;
    [SerializeField] protected float m_maxHealth;
    public float ImaxHealth { get; set; }
    public float IcurrentHealth { get; set; }
    public bool IisBlocking { get; set; }
    public bool Iinvulnerable { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        ImaxHealth = m_maxHealth;
        IcurrentHealth = ImaxHealth;
        IisBlocking = false;
        Iinvulnerable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnTakeDamage(float damage)
    {
    }

    public virtual void Die()
    {
        Destroy(gameObject);
        Instantiate(m_explosionVFX);
    }
}
