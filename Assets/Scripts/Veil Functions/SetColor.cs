﻿using UnityEngine;
using System.Collections;

public class SetColor : VeilObjectsController
{

    public Color veilOnColor = Color.white;
    public Color veilOffColor = Color.white;

	public Color lerpedColorOn;

	public float Lerp;
	public float fadetimeON;
	public float fadetimeOFF;


	void Update() {

		if (GameManager.veilOn) {
			if (Lerp < 1f) {
				Lerp += Time.deltaTime/ fadetimeON;
			}
			MeshRenderer renderer = GetComponent<MeshRenderer>();
			lerpedColorOn = Color.Lerp(veilOnColor, veilOffColor, Lerp);
			renderer.material.color = lerpedColorOn;
		}

		if (!GameManager.veilOn) {
			if (Lerp > 0f) {
				Lerp -= Time.deltaTime/fadetimeOFF;
			}
			MeshRenderer renderer = GetComponent<MeshRenderer>();
			lerpedColorOn = Color.Lerp(veilOnColor, veilOffColor, Lerp);
			renderer.material.color = lerpedColorOn;
		}
	

	
	}
}
