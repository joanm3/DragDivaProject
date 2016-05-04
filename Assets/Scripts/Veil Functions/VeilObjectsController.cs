using UnityEngine;
using System.Collections;

public class VeilObjectsController : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GameManager.Notifications.AddListener(this, "VeilOn");
        GameManager.Notifications.AddListener(this, "VeilOff");

    }
	

}
