﻿using UnityEngine;
using System.Collections;

public class MaterialFadeOut : VeilObjectsController {


	public float fadetimeON;
	public float fadetimeOFF;

	void Start(){
		
		Color color = this.GetComponent<Renderer>().material.color;
		color.a = -2f;
		this.GetComponent<Renderer>().material.color = color;
	}

	void Update(){

		if(GameManager.veilOn){
			

			Color color = this.GetComponent<Renderer>().material.color;
			if(color.a > -1f){
				color.a -= Time.deltaTime/fadetimeON;}
			this.GetComponent<Renderer>().material.color = color;

		}

		if(!GameManager.veilOn){
		

			Color color = this.GetComponent<Renderer>().material.color;
			if(color.a < 1f){
				color.a += Time.deltaTime/fadetimeOFF;}
			this.GetComponent<Renderer>().material.color = color;

		}

	}
		
}