﻿using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic; 

[CustomEditor(typeof(ObjectController))]
public class ObjectControllerEditor : Editor {


    private Dictionary<string, SerializedProperty> _properties = new Dictionary<string, SerializedProperty>();
    private List<Property> _timingProperties = new List<Property>();

    private class Property
    {
        public string name;
        public string text;

        public Property(string n, string t)
        {
            name = n;
            text = t;
        }
    }


    private readonly Property OBJECT_TYPE = new Property("thisType", "Object Seen In:");
    private readonly Property IS_STATIC = new Property("m_static", "Static:");
    private readonly Property IS_DRAGGABLE = new Property("isDraggable", "Draggable:");
	private readonly Property IS_VEILED = new Property("isVeiled", "Veiled:");
	private readonly Property START_VISIBLE = new Property("startVisible", "Start Visible:");
    private readonly Property MATERIAL_STANDARD = new Property("m_standardMaterial", "Standard Material:");
    private readonly Property MATERIAL_STENCIL = new Property("m_stencilMaterial", "Stencil Material:");



    private void OnEnable()
    {
        if (EditorApplication.isPlaying) return;
        _properties.Clear();
        SerializedProperty property = serializedObject.GetIterator();

        while (property.NextVisible(true))
        {
            _properties[property.name] = property.Copy();
        }

    }

    public override void OnInspectorGUI()
    {
        if (EditorApplication.isPlaying) return;

        serializedObject.Update();
        _timingProperties.Clear();

        ObjectController oc = target as ObjectController; 


        DisplayRegularField(OBJECT_TYPE);
        switch (oc.thisType)
        {
            case ObjectController.ObjectType.standard:
                oc.SetType(LayerMask.NameToLayer("Default"), oc.m_standardMaterial);
                oc.DestroyChildren(ObjectController.tagName);
                DisplayRegularInspector(); 
                break;
            case ObjectController.ObjectType.world:
                oc.SetType(LayerMask.NameToLayer("World"), oc.m_standardMaterial);
                oc.DestroyChildren(ObjectController.tagName);
                DisplayRegularInspector();
                break;
            case ObjectController.ObjectType.mirror:
                oc.SetType(LayerMask.NameToLayer("Mirror"), oc.m_standardMaterial);
                oc.DestroyChildren(ObjectController.tagName);
                DisplayRegularInspector();
                break;
            case ObjectController.ObjectType.window:
                oc.SetType(LayerMask.NameToLayer("Default"),oc.m_stencilMaterial);
                oc.DestroyChildren(ObjectController.tagName);
                DisplayRegularInspector();
                break;
            case ObjectController.ObjectType.veil:
                oc.SetType(LayerMask.NameToLayer("Veil"), oc.m_standardMaterial);
                oc.DestroyChildren(ObjectController.tagName);
                DisplayRegularInspector();
                break;
            case ObjectController.ObjectType.mirrorWindow:
                oc.SetType(LayerMask.NameToLayer("Mirror"), oc.m_standardMaterial);
                oc.CreateChild(ObjectController.ObjectType.window);
                DisplayRegularInspector();
                break;
            case ObjectController.ObjectType.allReflections:
                oc.SetType(LayerMask.NameToLayer("AllReflections"), oc.m_standardMaterial);
                oc.CreateChild(ObjectController.ObjectType.window);
                DisplayRegularInspector();
                break;
            case ObjectController.ObjectType.screenProjection:
                oc.SetType(LayerMask.NameToLayer("Projection"), oc.m_standardMaterial);
                DisplayRegularInspector();
                break;
            case ObjectController.ObjectType.Invisible:
                oc.SetType(LayerMask.NameToLayer("Invisible"), oc.m_standardMaterial);
                oc.DestroyChildren(ObjectController.tagName);
                DisplayRegularInspector();
                break;

        }

        serializedObject.ApplyModifiedProperties();

    }

    private void DisplayRegularInspector()
    {

        ObjectController oc = target as ObjectController;

        DisplayRegularField(IS_STATIC);
        if(!oc.m_static)
        DisplayRegularField(IS_DRAGGABLE);
		DisplayRegularField(IS_VEILED);
		DisplayRegularField(START_VISIBLE);


        DisplayRegularField(MATERIAL_STANDARD);
        DisplayRegularField(MATERIAL_STENCIL);
        GUILayout.Label("Not yet implemented in this script");
        GUILayout.Label("add the component manually");
        GUILayout.Button("Add Movement"); 

    }


    private void DisplayRegularField(Property property)
    {
        EditorGUILayout.PropertyField(_properties[property.name], new GUIContent(property.text));

    }




}
