using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : CharacterController1
{

    //https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        LookAtDirection(h);
        Move(new Vector3(h, 0 , v));

        if (Input.GetButtonDown("Fire1"))
        {
            // TODO: play attack animation
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
}
