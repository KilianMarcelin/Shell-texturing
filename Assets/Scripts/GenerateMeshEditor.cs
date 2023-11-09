using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GenerateMesh))]
public class GenerateMeshEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GenerateMesh genMesh = (GenerateMesh)target;
        if (DrawDefaultInspector())
        {
            //if(genMesh.autoUpdate) genMesh.Generate();
        }
        if (GUILayout.Button("Generate"))
        {
            genMesh.Generate();
        }
    }
}
