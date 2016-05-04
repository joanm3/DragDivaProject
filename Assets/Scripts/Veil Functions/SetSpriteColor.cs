using UnityEngine;
using System.Collections;

public class SetSpriteColor : VeilObjectsController
{


    public Color veilOnColor = Color.white;
    public Color veilOffColor = Color.white;


    void VeilOn()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.material.color = veilOnColor;


    }


    void VeilOff()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.material.color = veilOffColor;
    }


}
