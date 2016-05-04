using UnityEngine;
using System.Collections;

public class GrabNDrag : MonoBehaviour {

	public KeyCode grabDrag = KeyCode.E;
	public float speed;
	public bool grabbing;
	private Collider col_;

	void Start(){

		grabbing = false;
	}
	
	// Update is called once per frame
	void OnTriggerStay (Collider _col) {

        if (_col.GetComponent<ObjectClass>() == null)
            return; 

		if(_col.GetComponent<ObjectClass>().canBeDragged == true){

			print("p1");
			
			if(Input.GetKeyDown(grabDrag)){

				print("p2");

				grabbing = !grabbing;

				col_ = _col;
			}


		}
	
	}

	void Update(){

		if(grabbing){
			Dragouille();
		}

	}

	public void Dragouille(){
		
		print("p3");
			float step = speed * Time.deltaTime;
			col_.transform.position = Vector3.MoveTowards(col_.transform.position, this.transform.position, step);

	}
}
