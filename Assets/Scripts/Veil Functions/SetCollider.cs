using UnityEngine;
using System.Collections;

public class SetCollider : VeilObjectsController {

    public bool veilOnColliderActive;
    public bool veilOffColliderActive; 

    void VeilOn()
    {
        //Find collider component
        Collider collider = GetComponent<BoxCollider>();
        if (!collider)
            collider = GetComponent<SphereCollider>();
        //if collider exists, then set enabled collider or not
        if (collider)
            collider.enabled = veilOnColliderActive; 
         
    }

    void VeilOff()
    {
        //Find collider component
        Collider collider = GetComponent<BoxCollider>();
        if (!collider)
            collider = GetComponent<SphereCollider>();
        //if collider exists, then set enabled collider or not
        if (collider)
            collider.enabled = veilOffColliderActive;


    }


}
