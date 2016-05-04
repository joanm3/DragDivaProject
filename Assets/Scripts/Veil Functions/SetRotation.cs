﻿using UnityEngine;
using System.Collections;

public class SetRotation : VeilObjectsController
{

    public bool veilOnRotating;
    public bool veilOffRotating;
    public float velocity = 1f;


    void LateUpdate()
    {
        if (GameManager.veilOn)
        {
            if (veilOnRotating)
                RotateFunction();

        }
        else
            if (veilOffRotating)
            RotateFunction();


    }

    void RotateFunction()
    {
        transform.Rotate(Vector3.right * Time.deltaTime * velocity); 
    }


}
