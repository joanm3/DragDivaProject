using UnityEngine;
using System.Collections;

public class GrabNDrag : MonoBehaviour {

	public KeyCode grabDrag = KeyCode.E;
	public float speed;
	public bool grabbing;

	void Start(){

		grabbing = false;
	}
	
	// Update is called once per frame
	void OnTriggerStay (Collider _col) {

        if (_col.GetComponent<ObjectController>() == null)
            return; 

		if(_col.GetComponent<ObjectController>().isDraggable == true){
			
			if(Input.GetKeyDown(grabDrag)){

				grabbing = !grabbing;
				Dragouille(_col);
			}


		}
	
	}

	public void Dragouille(Collider col_){


		//2 way of doing this

		//1:
			//float step = speed * Time.deltaTime;
			//col_.transform.position = Vector3.MoveTowards(col_.transform.position, this.transform.position, step);

		//2:
		if(grabbing){
			col_.transform.parent = this.transform;}
		if(!grabbing){
			this.transform.GetChild(0).transform.parent = null;
		}

	}
}
