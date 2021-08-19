using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Changelog
/* Inital Script created by Oliver (05/08/21)
 * Not a production script, script created for our environment artist to use so that she could film a video with smooth camera motion
 */
public class CamFollowMovment : MonoBehaviour
{
    //Variable public so artist could control camera speed
    [Range(0, 25)] public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0f, 0f);
    }
}
