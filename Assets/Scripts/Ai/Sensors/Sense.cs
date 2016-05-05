using UnityEngine;
using System.Collections;

public class Sense : MonoBehaviour {


    public Aspect.aspect aspectName = Aspect.aspect.Player;
    public float detectionRate = 0.0f;
    [HideInInspector]
    public Wander wander; 

    protected float elapsedTime = 0.0f;

    protected virtual void Initialize() { }
    protected virtual void UpdateSense() { }
   


	void Start () {

        elapsedTime = 0.0f;
        wander = GetComponent<Wander>(); 
        Initialize();	
	}
	
	// Update is called once per frame
	void Update () {
        UpdateSense();
	
	}
}
