using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{

    public bool veilOnBillboarding = true;
    public bool veilOffBillboarding = true;

	public float toMe;


    private Transform thisTransform = null;

    void Start()
    {

        thisTransform = transform;


    }

    void LateUpdate()
    {
        if (GameManager.veilOn)
        {
            if (veilOnBillboarding)
                BillboardFunction();

        }
        else
            if (veilOffBillboarding)
            BillboardFunction();


    }


    void BillboardFunction()
    {
        //the sprite faces the camera
		Vector3 LookAtDir = new Vector3(Camera.main.transform.position.x - thisTransform.position.x, 0, Camera.main.transform.position.z - thisTransform.position.z);
        thisTransform.rotation = Quaternion.LookRotation(toMe * LookAtDir.normalized, Vector3.up);
    }
}
