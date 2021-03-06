﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ReflectionProbe))]
//[ExecuteInEditMode]
public class ControlProbe : MonoBehaviour
{
    [SerializeField]
    private float m_yOffset = 0.5f;
    public Vector3 Normal;

    private float m_offset;
    private GameObject m_mirror;
    private Vector3 m_symmetricPoint;
    private GameObject m_playerCamera;
    private Mesh m_mesh;


    void Start()
    {

        m_mirror = transform.parent.gameObject;
        if (m_mirror == null)
            Debug.LogError("No Mirror GameObject Found for " + gameObject.name);


        //GET COMPONENTS
        m_playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (m_playerCamera == null)
            Debug.LogError("No Player GameObject Found for " + gameObject.name);

        m_mirror = transform.parent.gameObject;
        if (m_mirror == null)
            Debug.LogError("No Mirror GameObject Found for " + gameObject.name);

        m_mesh = this.GetComponentInParent<MeshFilter>().sharedMesh;

        //CALCULATE NORMALS
        m_mesh.RecalculateNormals();
        Normal = m_mesh.normals[1];
        Normal = this.transform.parent.transform.TransformVector(Normal);

    }


    void FixedUpdate()
    {

        //UPDATE POSITION TO SYMMETRIC POINT
        Plane plane = new Plane(Normal, transform.parent.transform.position);
        Vector3 projectionPlane = Formulas.ProjectPointOnPlane(m_playerCamera.transform.position, plane);
        m_symmetricPoint = projectionPlane - (m_playerCamera.transform.position - projectionPlane);
        transform.position = new Vector3(m_symmetricPoint.x, m_symmetricPoint.y + m_yOffset, m_symmetricPoint.z);
       // Vector3 cameraDirection = m_playerCamera.transform.forward;
       // float scalarProduct = Vector3.Dot(Normal, cameraDirection);
       // Vector3 mirroredDirection = cameraDirection - (2 * scalarProduct * Normal.normalized);
        // transform.LookAt(transform.position + mirroredDirection);


    }




}
