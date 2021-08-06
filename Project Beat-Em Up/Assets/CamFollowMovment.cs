using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowMovment : MonoBehaviour
{
    [Range(0, 25)] public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0f, 0f);
    }
}
