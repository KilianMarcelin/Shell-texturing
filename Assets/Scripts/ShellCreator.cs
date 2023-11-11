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
    public int density = 100;
    public float shellDistance = 0.1f;
    public float thickness = 1f;
    public void CreateShells()
    {
        DeleteShells();
        
        shells = new GameObject[shellCount];
        for (int i = 0; i < shellCount; ++i)
        {
            Material mat = new Material(shellShader);
            shells[i] = new GameObject("Shell " + i.ToString());
            shells[i].AddComponent(typeof(MeshRenderer));
            shells[i].AddComponent(typeof(MeshFilter));
            shells[i].AddComponent(typeof(GenerateMesh));
            shells[i].GetComponent<MeshRenderer>().sharedMaterial = mat;
            shells[i].GetComponent<GenerateMesh>().Generate(((float)i/shellCount)*shellDistance);
            shells[i].GetComponent<MeshRenderer>().sharedMaterial.SetInt("_ShellCount", shellCount);
            shells[i].GetComponent<MeshRenderer>().sharedMaterial.SetInt("_ShellIndex", i);
            shells[i].GetComponent<MeshRenderer>().sharedMaterial.SetInt("_Density", density);
            shells[i].GetComponent<MeshRenderer>().sharedMaterial.SetColor("_BaseColor", shellColor);
            shells[i].GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Thickness", thickness);

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
