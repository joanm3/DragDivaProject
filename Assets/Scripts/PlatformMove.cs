using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformMove : MonoBehaviour {

	public int markcount;
	public float speed;
	public List<Transform> targets = new List<Transform>();

	// Use this for initialization
	void Start () {
		markcount = -1;
	}

	// Update is called once per frame
	void Update () {

		float step = speed * Time.deltaTime;
		this.transform.position = Vector3.MoveTowards(this.transform.position, targets[markcount+1].transform.position, step);

		}

	void OnTriggerEnter(Collider other) {

		if (other.name.Contains("Mark")) {
			markcount += 1;
			if(markcount == targets.Count-1){
				markcount = -1;
			}
		}

	}
}