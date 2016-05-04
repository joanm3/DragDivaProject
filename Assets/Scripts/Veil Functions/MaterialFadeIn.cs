using UnityEngine;
using System.Collections;

public class MaterialFadeIn : VeilObjectsController {


	public float fadetimeON;
	public float fadetimeOFF;


	void Start(){

        GameManager.Notifications.AddListener(this, "VeilOn");
        GameManager.Notifications.AddListener(this, "VeilOff");

        Color color = this.GetComponent<Renderer>().material.color;
		color.a = 1f;
		this.GetComponent<Renderer>().material.color = color;
	}




    void VeilOn()
    {
      //  StopCoroutine(FadeOut());
        StartCoroutine(FadeIn()); 
    }


    void VeilOff()
    {
      //  StopCoroutine(FadeIn());
        StartCoroutine(FadeOut()); 

    }

    IEnumerator FadeIn()
    {
        Debug.Log("FadeIn entered"); 
        while (true)
        {
            Color color = this.GetComponent<Renderer>().material.color;
            if (color.a <= 1f)
            {
				print("fadein enter");
                color.a += Time.deltaTime / fadetimeON;
            }
            this.GetComponent<Renderer>().material.color = color;
            yield return null; 
        }

    }

    IEnumerator FadeOut()
    {
        while (true)
        {
        Debug.Log("FadeOut entered");
            Color color = this.GetComponent<Renderer>().material.color;
            if (color.a > -1f)
            {
                Debug.Log("if entered");
                color.a -= Time.deltaTime / fadetimeOFF;
            }
            this.GetComponent<Renderer>().material.color = color;
            yield return null;
        }

    }

}