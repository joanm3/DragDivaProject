﻿using UnityEngine;
using System.Collections;

public class SetVisible : VeilObjectsController
{

    public bool veilOnVisible = true;
    public bool veilOffVisible = true;

   // public float fadetimeON;
   // public float fadetimeOFF;

    private MeshRenderer m_renderer;

    void Awake()
    {
        m_renderer = GetComponent<MeshRenderer>();

    }



    void VeilOn()
    {
        m_renderer = GetComponent<MeshRenderer>();
        m_renderer.enabled = veilOnVisible;
    }

    void VeilOff()
    {
        m_renderer = GetComponent<MeshRenderer>();
        m_renderer.enabled = veilOffVisible;

    }





    //void VeilOn2()
    //{
    //    StopAllCoroutines();
    //    StartCoroutine(FadeIn());
    //}


    //void VeilOff2()
    //{
    //    StopAllCoroutines();
    //    StartCoroutine(FadeOut());

    //}

    //IEnumerator FadeIn()
    //{
    //    while (true)
    //    {
    //        Color color = this.GetComponent<Renderer>().material.color;
    //        if (color.a <= 1f)
    //        {
    //            color.a += Time.deltaTime / fadetimeON;
    //        }
    //        this.GetComponent<Renderer>().material.color = color;
    //    }

    //}

    //IEnumerator FadeOut()
    //{
    //    while (true)
    //    {
    //        Color color = this.GetComponent<Renderer>().material.color;
    //        if (color.a > -1f)
    //        {
    //            color.a -= Time.deltaTime / fadetimeOFF;
    //        }
    //        this.GetComponent<Renderer>().material.color = color;
    //    }

    //}




}
