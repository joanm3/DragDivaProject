using UnityEngine;
using System.Collections;

public class MenuFunctions : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void LoadLevel () {

		Application.LoadLevel("Proto GD");
	
	}

	public void Quitting (){

		Application.Quit();
	}
}
