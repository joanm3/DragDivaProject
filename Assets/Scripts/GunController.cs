using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

   // public GameObject bullet;
    public ObjectPoolScript thisPool;
    public float fireRate = 1f;
    public bool bdebug = false;
    private float nextFire;



    void Start()
    {
        if (thisPool == null)
            Debug.LogError("Couldnt find the ObjectPoolScript for " + name); 


    }


    void Update () {

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Fire(); 
        }
    }



    public bool Fire()
    {
        GameObject obj = thisPool.GetPooledObject();
        if (obj == null)
            return false;

        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        BulletController bc = obj.GetComponent<BulletController>();
        if(bc != null)
        bc.shooterTransform = this.transform; 

        obj.SetActive(true);
        return true;
    }





}
