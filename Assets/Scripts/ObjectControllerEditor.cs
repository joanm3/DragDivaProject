using UnityEngine;
using System.Collections;
using UnityEditor; 

[CustomEditor(typeof(ObjectController))]
public class ObjectControllerEditor : Editor {


    public enum ObjectType
    {
        everywhere, world, mirror, window, veil, mirrorWindow, allReflections, screenProjection, Invisible
    };

    ObjectType thisType = ObjectType.everywhere;

    public override void OnInspectorGUI()
    {
        ObjectController oc = target as ObjectController; 

        thisType = (ObjectType) EditorGUILayout.EnumPopup("Object Seen In", thisType); 

        switch(thisType)
        {
            case ObjectType.everywhere:
                oc.SetType(LayerMask.NameToLayer("Default"), oc.standardMaterial); 
                break;
            case ObjectType.world:
                oc.SetType(LayerMask.NameToLayer("World"), oc.standardMaterial);
                break;
            case ObjectType.mirror:
                oc.SetType(LayerMask.NameToLayer("Mirror"), oc.standardMaterial);
                break;
            case ObjectType.window:
                oc.SetType(LayerMask.NameToLayer("Default"),oc.stencilMaterial);
                break;
            case ObjectType.veil:
                oc.SetType(LayerMask.NameToLayer("Veil"), oc.standardMaterial);
                break;
            case ObjectType.mirrorWindow:
                oc.SetType(LayerMask.NameToLayer("Mirror"), oc.standardMaterial);
                break;
            case ObjectType.allReflections:
                oc.SetType(LayerMask.NameToLayer("AllReflections"), oc.standardMaterial);
                break;
            case ObjectType.screenProjection:
                oc.SetType(LayerMask.NameToLayer("Projection"), oc.standardMaterial);
                break;
            case ObjectType.Invisible:
                oc.SetType(LayerMask.NameToLayer("Invisible"), oc.standardMaterial);
                break;

        }

    }




}
