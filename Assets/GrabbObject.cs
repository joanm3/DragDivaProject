using UnityEngine;
using System.Collections;

public class GrabbObject : MonoBehaviour {

    [SerializeField]
    Color grabbColor; 

    private bool m_isGrabbing = false;
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
        m_objectToGrabb = null;
        Debug.Log("Object to dragg is " + m_objectToGrabb);

    }

    void Update()
    {

        Debug.Log("isGrabbing = " + m_isGrabbing); 

        if (m_objectToGrabb == null)
            return;

        if(!m_isGrabbing && Input.GetButtonDown("Grabb"))
        {
            Debug.Log("Grabb button touched"); 
            m_objectToGrabb.SetParent(this.transform);
           // m_objectToGrabb.SetParent(GameObject.FindGameObjectWithTag("MainCamera").transform);
            m_isGrabbing = true; 
        }

       else if (m_isGrabbing && Input.GetButtonDown("Grabb"))
        {
           // Debug.Log("Grabb button touched");
            m_objectToGrabb.parent = null;
            m_isGrabbing = false;

        }
    } 


}
