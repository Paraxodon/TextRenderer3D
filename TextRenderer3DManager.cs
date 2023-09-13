using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

//Made by paraxodon :D

[ExecuteInEditMode]
public class TextRenderer3DManager : MonoBehaviour
{
    public static TextRenderer3DManager Instance;
    
    [SerializeField] private string textMeshesFolder = "TextModels";
    
    public List<GameObject> letters;
    [HideInInspector] public bool hasLetters;
    

    private void OnValidate()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            DestroyImmediate(gameObject);
        }
    }
    
    public void GetLetters()
    {
        letters = new List<GameObject>();
        foreach (var file in SearchForFolder(Application.dataPath,textMeshesFolder).GetFiles())
        {
            var extension = file.Extension;
            if (extension != ".obj") continue;

            var filePath = "Assets" + file.FullName.Substring(Application.dataPath.Length);
            var obj = AssetDatabase.LoadAssetAtPath<GameObject>(filePath);
            
            letters.Add(obj);
        }

        Resources.UnloadUnusedAssets();
        hasLetters = letters != null;
    }

    public GameObject GetLetter(char character)
    {
        foreach (var c in letters)
        {
            if (c.name.ToCharArray()[0] == (character))
            {
                return c;
            }
        }
        
        Debug.LogError("Letter doesn't exist");
        
        return new GameObject(" ");
    }
    
    public static DirectoryInfo SearchForFolder(string initialPath, string folderName)
    {
        var initialDirectory = new DirectoryInfo(initialPath);

        foreach (var directory in initialDirectory.GetDirectories())
        {
            if (directory.Name.Equals(folderName))
            {
                return directory;
            }
            
            var nextDir = SearchForFolder(directory.FullName, folderName);
            if (nextDir != null)
            {
                return nextDir;
            }
        }
        
        return null;
    }
}
