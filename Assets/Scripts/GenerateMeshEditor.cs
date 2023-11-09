using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShellCreator))]
public class GenerateMeshEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ShellCreator shellCreator = (ShellCreator)target;
        if (DrawDefaultInspector())
        {
        }
        if (GUILayout.Button("Generate Shells"))
        {
            shellCreator.CreateShells();
        }
        if (GUILayout.Button("Delete Shells"))
        {
            shellCreator.DeleteShells();
        }
    }
}
