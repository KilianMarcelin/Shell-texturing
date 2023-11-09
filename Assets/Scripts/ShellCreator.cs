using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;

public class ShellCreator : MonoBehaviour
{
    private GameObject[] shells;
    public Color shellColor = Color.green;
    public int shellCount;
    public Shader shellShader;
    public void CreateShells()
    {
        DeleteShells();
        Material mat = new Material(shellShader);
        shells = new GameObject[shellCount];
        for (int i = 0; i < shellCount; ++i)
        {
            shells[i] = new GameObject("Shell " + i.ToString());
            shells[i].AddComponent(typeof(MeshRenderer));
            shells[i].AddComponent(typeof(MeshFilter));
            shells[i].AddComponent(typeof(GenerateMesh));
            shells[i].GetComponent<MeshRenderer>().material = mat;
            shells[i].GetComponent<GenerateMesh>().Generate(((float)i/shellCount)*0.1f);
            shells[i].GetComponent<MeshRenderer>().material.SetInt("_ShellCount", shellCount);
            shells[i].GetComponent<MeshRenderer>().material.SetInt("_ShellIndex", i);
            shells[i].GetComponent<MeshRenderer>().material.SetInt("_Density", 100);
            shells[i].GetComponent<MeshRenderer>().material.SetColor("_BaseColor", shellColor);
            shells[i].transform.SetParent(this.transform);
        }
    }

    public void DeleteShells()
    {
        for (int i = transform.childCount-1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
