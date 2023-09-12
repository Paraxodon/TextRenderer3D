using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextRenderer3DManager))]
public class TextRenderer3DManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        var component = (TextRenderer3DManager)target;
        
        EditorGUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Get Meshes From Folder Name"))
        {
            component.GetLetters();
        }
        
        EditorGUILayout.EndHorizontal();
        
        var hasLetters = component.hasLetters;
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.Toggle("Ready", hasLetters);
        EditorGUI.EndDisabledGroup();
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }
}
