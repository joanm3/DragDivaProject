using UnityEngine;
using System.Collections;

public class FlipCamera : MonoBehaviour {

	void Start () {

        Camera camera = GetComponent<Camera>(); 
        Matrix4x4 mat = camera.projectionMatrix;
        mat *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
        camera.projectionMatrix = mat;

    }
	
	
}
