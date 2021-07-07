using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitDemo : MonoBehaviour, IDamagable_Oliver
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material HitMat;
    private float hp;
    public bool m_isBlocking;
    public float ImaxHealth { get; set; }
    public float IcurrentHealth { get; set; }
    public bool IisBlocking { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        hp = 2;
        ImaxHealth = hp;
        IcurrentHealth = ImaxHealth;
        IisBlocking = m_isBlocking;
    }

    public void OnTakeDamage(float damage)
    {
        if (IisBlocking == true)
        {
            damage /= 2;
        }
        if(IcurrentHealth < 2 && IcurrentHealth > 0)
        {
            meshRenderer.material = HitMat;
        }
        //todo: some particles, sounds and animations
    }

    public void Die()
    {
        //debug:
        Destroy(gameObject);
        //todo: some particles, sounds and animations
    }

    //private void UpdateObj()
    //{
    //    if(hp == 1)
    //    {
    //        meshRenderer.material = HitMat;
    //    }
    //    if(hp == 0)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
