﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ObjectController : MonoBehaviour
{


    //TO DO: CREATE A NEW STENCIL MATERIAL IF IT DOESNT EXIST FROM THE STANDARD MATERIAL (IF THERE IS ONE)

    public const string tagName = "Generated";

    public enum ObjectType
    {
        standard, world, mirror, window, veil, mirrorWindow, allReflections, screenProjection, Invisible
    };

    public ObjectType thisType = ObjectType.standard;

    private bool m_childCreated = false;

    [SerializeField]
    internal bool m_static;

	public bool isVeiled = true;
	public bool startVisible;
    public bool isDraggable;

    [SerializeField]
    internal Material m_standardMaterial = null;

    [SerializeField]
    internal Material m_stencilMaterial = null;



    public void OnEnable()
    {
        //   MeshRenderer renderer = GetComponent<MeshRenderer>(); 
        /*
                switch (thisType)
                {
                    case ObjectType.standard:
                        renderer.material = m_standardMaterial;
                        break;
                    case ObjectType.world:
                        renderer.material = m_standardMaterial;
                        break;
                    case ObjectType.mirror:
                        renderer.material = m_standardMaterial;
                        break;
                    case ObjectType.window:
                        renderer.material = m_stencilMaterial;
                        break;
                    case ObjectType.veil:
                        renderer.material = m_standardMaterial;
                        break;
                    case ObjectType.mirrorWindow:
                        renderer.material = m_standardMaterial;
                        break;
                    case ObjectType.allReflections:
                        renderer.material = m_standardMaterial;
                        break;
                    case ObjectType.screenProjection:
                        renderer.material = m_standardMaterial;
                        break;
                    case ObjectType.Invisible:
                        renderer.material = m_standardMaterial;
                        break;

                }
                */

    }



    public void SetType(LayerMask layer, Material material)
    {
        SetLayer(layer);
        ApplyMaterial(material);
		IsVeiled();
        gameObject.isStatic = (m_static == true) ? true : false; 

    }

    public void CreateChild(ObjectType type)
    {
        //no funciona perque el canviara a cada cop a false. sha de canviar. 

        if (!m_childCreated)
        {
            GameObject childObject = new GameObject();
            childObject.transform.parent = gameObject.transform;
            childObject.transform.position = Vector3.zero;
            childObject.transform.rotation = Quaternion.identity;
            childObject.transform.localScale = Vector3.one;
            childObject.tag = ObjectController.tagName;
            // childObject.name = name + "Window"; 
            MeshRenderer mr = childObject.AddComponent<MeshRenderer>();
            ObjectController oc = childObject.AddComponent<ObjectController>();
            oc.thisType = type;
            oc.m_standardMaterial = m_standardMaterial;
            oc.m_stencilMaterial = m_stencilMaterial;
            oc.isDraggable = isDraggable;
            oc.m_static = m_static;
			oc.isVeiled = isVeiled;
            oc.SetType(LayerMask.NameToLayer("Default"), oc.m_stencilMaterial);
            m_childCreated = true;
        }



    }

    public void DestroyChildren(string tagName)
    {
        foreach (Transform child in transform)
        {
            if (child.tag == tagName)
                DestroyImmediate(child.gameObject);
        }
        m_childCreated = false;

    }


    private void SetLayer(LayerMask layer)
    {
        gameObject.layer = layer;
    }


    private void ApplyMaterial(Material material)
    {
        if (GetComponent<MeshRenderer>() != null)
            if (material != null)
            {
                GetComponent<MeshRenderer>().material = material;
            }
    }

    private void resetBools()
    {
        isDraggable = false;
        m_static = true;

    }

    private void SetStatic()
    {
        gameObject.isStatic = m_static;
    }

	public void IsVeiled(){


        MaterialFadeIn mfi = this.GetComponent<MaterialFadeIn>();
        if (mfi == null)
            return; 

        if (isVeiled){
			mfi.enabled = true;
		}else{
			mfi.enabled = false;
		}
	}


}
