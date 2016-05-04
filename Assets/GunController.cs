using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

    public GameObject bullet;
    public float fireRate = 1f;
    public bool bdebug = false;
    private float nextFire; 

	// Use this for initialization
	void Start () {
        if(!bdebug)
        Cursor.lockState = CursorLockMode.Locked; 
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bullet, transform.position, transform.rotation);
        }

    }





}
