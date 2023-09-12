using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TextRenderer3D))]
public class TextRenderer3DEditor : Editor
{
    private SerializedProperty text3D;

    private void OnEnable()
    {
        text3D = serializedObject.FindProperty("text");
    }

    public override void OnInspectorGUI()
    {
        var component = (TextRenderer3D)target;
        
        float customHeight = 100;

        Rect customRect = EditorGUILayout.GetControlRect(false, customHeight, EditorStyles.textField);
        
        EditorGUI.BeginChangeCheck();
        var newString = EditorGUI.TextField(customRect, new GUIContent("Enter Text"), component.text);
        if (EditorGUI.EndChangeCheck())
        {
            text3D.stringValue = newString;
            serializedObject.ApplyModifiedProperties();
            
            component.CreateLetters();
        }
        
        GUILayout.Space(30);
        
        base.OnInspectorGUI();
        
        GUILayout.Space(30);
    }
}
