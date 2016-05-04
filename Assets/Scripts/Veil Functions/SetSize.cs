using UnityEngine;
using System.Collections;

public class SetSize : VeilObjectsController {

    public Vector3 veilOnSize;
    public Vector3 veilOffSize;


    void VeilOn()
    {
        transform.localScale = veilOnSize; 
    }

    void VeilOff()
    {
        transform.localScale = veilOffSize; 

    }
}
