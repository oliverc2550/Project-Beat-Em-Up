using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitDemo : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material HitMat;
    private float hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = 2;
    }

    public void TakeDamage()
    {
        hp--;
        UpdateObj();
    }

    private void UpdateObj()
    {
        if(hp == 1)
        {
            meshRenderer.material = HitMat;
        }
        if(hp == 0)
        {
            Destroy(gameObject);
        }
    }
}
