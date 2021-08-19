using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Changelog
/* Inital Script created by Oliver (04/07/21)
 * Not a production script, script created to test the IDamagable Interface at runtime
 */
public class AttackHitDemo : MonoBehaviour, IDamagable
{
    #region Variables
    //Variables seralized so the could be set in the editor
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material HitMat;
    private float hp;
    public bool m_isBlocking;
    public float ImaxHealth { get; set; }
    public float IcurrentHealth { get; set; }
    public bool IisBlocking { get; set; }
    public bool Iinvulnerable { get; set; }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        hp = 2;
        ImaxHealth = hp;
        IcurrentHealth = ImaxHealth;
        IisBlocking = m_isBlocking;
    }
    //Interface Methods
    public void OnTakeDamage(float damage)
    {
        if(IcurrentHealth < 2 && IcurrentHealth > 0)
        {
            meshRenderer.material = HitMat;
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
