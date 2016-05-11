using UnityEngine;
using System.Collections;

public class GrabbObject : MonoBehaviour {

    [SerializeField]
    Color grabbColor;
    [SerializeField]
    private bool bDebug = false; 

    [SerializeField]
    private bool m_isGrabbing = false;
    [SerializeField]
    private Transform m_objectToGrabb; 


    void Start()
    {
        m_isGrabbing = false; 
    }

    void OnTriggerEnter(Collider other)
    {

        ObjectController oc = other.GetComponent<ObjectController>();

        if (oc == null)
            return;

        if (oc.isDraggable == false)
            return;


        m_objectToGrabb = other.gameObject.transform;
        Material material = m_objectToGrabb.GetComponent<MeshRenderer>().material;
        material.color = grabbColor;
        if(bDebug)
        Debug.Log("Object to dragg is " + m_objectToGrabb); 

    }

    void OnTriggerExit(Collider other)
    {
        ObjectController oc = other.GetComponent<ObjectController>();

        if (oc == null)
            return;
        if (oc.isDraggable == false)
            return;
        Material material = m_objectToGrabb.GetComponent<MeshRenderer>().material;
        material.color = Color.white;
        if (bDebug)
            Debug.Log("Object to dragg is " + m_objectToGrabb);

    }

    void Update()
    {
        if (bDebug)
            Debug.Log( name + " isGrabbing = " + m_isGrabbing); 

        if (m_objectToGrabb == null)
            return;

        if(!m_isGrabbing && Input.GetButtonDown("Grabb"))
        {

            m_objectToGrabb.SetParent(this.transform);
            m_objectToGrabb.transform.position = this.transform.position;

           // m_objectToGrabb.SetParent(GameObject.FindGameObjectWithTag("MainCamera").transform);
            m_isGrabbing = true; 
        }

       else if (m_isGrabbing && Input.GetButtonDown("Grabb"))
        {
           // Debug.Log("Grabb button touched");
            m_objectToGrabb.parent = null;
            m_objectToGrabb = null;
            m_isGrabbing = false;

        }
    } 


}
