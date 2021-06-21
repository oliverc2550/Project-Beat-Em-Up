﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterController
{

    //https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        LookAtDirection(h);
        Move(new Vector2(h, 0));

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