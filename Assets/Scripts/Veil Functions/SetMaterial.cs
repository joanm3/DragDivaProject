using UnityEngine;
using System.Collections;

public class SetMaterial : VeilObjectsController
{


    public Material veilOnMaterial;
    public Material veilOffMaterial;

    void VeilOn()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material = veilOnMaterial;

    }

    void VeilOff()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material = veilOffMaterial;

    }



}
