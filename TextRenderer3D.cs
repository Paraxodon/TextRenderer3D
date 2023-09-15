using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Made by paraxodon :D

[ExecuteInEditMode]
public class TextRenderer3D : MonoBehaviour
{
    public bool allCaps;
    public bool allLowerCase = true;

    [Space(30)] 
    
    public float spacing = 3.5f;

    private float lastSpacing;
    
    [Space(5)] 
    
    public float iLetterSpacingChanger = 0.8f;
    
    [Space(5)] 
    
    public Vector3 startingRotation = new Vector3(0, -90, 0);

    private Vector3 lastRotation;
    
    [Space(30)]

    [SerializeField] private Material defaultMat;
    public List<GameObject> usedLetters;
    [HideInInspector] public string text = "";

    private void Start()
    {
        if (TextRenderer3DManager.Instance == null)
        {
            GameObject gO = new GameObject("TextRenderer3DManager");
            var textRenderer = gO.AddComponent<TextRenderer3DManager>();
            textRenderer.GetLetters();
        }
        else if(!TextRenderer3DManager.Instance.hasLetters)
        {
            TextRenderer3DManager.Instance.GetLetters();
        }
    }

    private void OnEnable()
    {
        Undo.undoRedoEvent += OnUndo;
    }
    
    private void OnDisable()
    {
        Undo.undoRedoEvent -= OnUndo;
    }

    private void Update()
    {
        if (lastSpacing == spacing && startingRotation == lastRotation) return;
        if (usedLetters == null)
        {
            usedLetters = new List<GameObject>();
        }
        CreateLetters();
        lastSpacing = spacing;
        lastRotation = startingRotation;
    }

    public void CreateLetters()
    {for (var i = usedLetters.Count - 1; i >= 0; i--) //Cleanup
        {
            try //This helps the undo
            {
                Undo.DestroyObjectImmediate(usedLetters[i]);
            }
            catch{/*ignored*/}
        }
        
        usedLetters.Clear();
        
        var startingScale = transform.localScale;
        transform.localScale = Vector3.one;
        
        text = allLowerCase ? text.ToLower() : allCaps ? text.ToUpper() : text;

        var previousLetter = gameObject;
        
        for (var i = 0; i < text.Length; i++) //Cleanup
        {
            var prev = i > 0 ? text[i - 1].ToString() : null;
            
            if (string.IsNullOrWhiteSpace(text[i].ToString()))
            {
                prev = " ";
                continue;
            }

            var usedSpacing = spacing;

            if (prev == "i" || text[i].ToString() == "i")
            {
                usedSpacing *= iLetterSpacingChanger;
            }
            else if (prev == " ")
            {
                usedSpacing *= 2;
            }

            var current = Instantiate(TextRenderer3DManager.Instance.GetLetter(text[i])
                , transform.position
                ,  transform.rotation);

            
            if (current == null){
                DestroyImmediate(current);
            return;
            }
            
            current.transform.SetParent(transform);

            current.transform.position = previousLetter.transform.position +
                                              transform.right * usedSpacing * (i == 0 ? 0 : 1);

            current.transform.localScale = transform.localScale;

            current.transform.Rotate(startingRotation);
            
            MeshRenderer mesh;

            if (!current.TryGetComponent(out mesh))
            {
                mesh = current.GetComponentInChildren<MeshRenderer>();
            }

            mesh.sharedMaterial = defaultMat;
            
            usedLetters.Add(current);
            
            Undo.RegisterCreatedObjectUndo(current, "Object" + i);

            previousLetter = current;
        }

        foreach (var lett in usedLetters)
        {
            Undo.RegisterCompleteObjectUndo(lett, "List");
        }
        
        transform.localScale = startingScale;
    }

    private void GetLettersAfterUndo()
    {
        var current = 0;
        
        for (int i = 0; i < transform.childCount && current < text.Length; i++)
        {
            var child = transform.GetChild(i);
            
            if (string.IsNullOrWhiteSpace(text[current].ToString())) continue;
            
            if (child.name.Substring(0,1) == text[current].ToString())
            {
                if (current < usedLetters.Count && usedLetters[i] != null)
                {
                    usedLetters[i] = child.gameObject;
                }
                else if (current >= usedLetters.Count)
                {
                    usedLetters.Add(child.gameObject);
                }
                
                current++;
                i = 0;
            }
        }
    }

    private void OnUndo(in UndoRedoInfo uRInfo)
    {
        GetLettersAfterUndo();
    }
}
