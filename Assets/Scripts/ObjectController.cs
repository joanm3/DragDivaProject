using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ObjectController : MonoBehaviour {





    [SerializeField]
    internal bool m_static; 

    [SerializeField]
    internal bool m_isDraggable;

    [SerializeField]
    internal Material standardMaterial = null;

    [SerializeField]
    internal Material stencilMaterial = null; 





    public void SetType(LayerMask layer, Material material)
    {
        SetLayer(layer);
        ApplyMaterial(material); 
        foreach(Transform child in transform)
        {
            if (child.tag == "Generated")
                DestroyImmediate(child); 
        }
    }


    private void SetLayer(LayerMask layer)
    {
        gameObject.layer = layer; 
    }


    private void ApplyMaterial(Material material)
    {
        GetComponent<MeshRenderer>().material = material; 
    }





}
