using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Changelog
/* Inital Script created by Oliver (18/08/21)
 * 18/08/21 Thea - Reworked code so that the animation would play properly after the barrel had taken enough damage to destroy it.
 */
public class ExplosiveBarrel : MonoBehaviour, IDamagable
{
    #region Variables
    [SerializeField] protected GameObject m_explosionVFX;
    [SerializeField] protected float m_maxHealth;
    public float ImaxHealth { get; set; }
    public float IcurrentHealth { get; set; }
    public bool IisBlocking { get; set; }
    public bool Iinvulnerable { get; set; }
    #endregion

    void Start()
    {
        ImaxHealth = m_maxHealth;
        IcurrentHealth = ImaxHealth;
        IisBlocking = false;
        Iinvulnerable = false;
    }

    #region On barrel hit
    // Coming from the interface.
    public virtual void OnTakeDamage(float damage)
    {
    }

    //The meshRenderer of the barrel is disabled because if the object itself is destroyed immediately, the VFX can never be destroyed or disabled because it is 
    //instantiated by the barrelObj.
    public virtual void Die()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(PlayVFX());
    }

    //After the explosionVFX is instantiated and played, the explosion is being destroyed. The main game object is destroyed after delay because otherwise the VFX gets
    //stuck in the last frame of its animation and it never gets destroyed or disabled.
    IEnumerator PlayVFX()
    {
        GameObject explosion = Instantiate(m_explosionVFX);

        yield return new WaitForSeconds(0.5f);
        Destroy(explosion);

        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    #endregion
}
