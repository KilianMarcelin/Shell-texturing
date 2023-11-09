using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMesh : MonoBehaviour
{
    public int vertexCount = 10;
    public Material material; // Ajoutez une référence au matériau dans l'inspecteur

    private Mesh mesh;

    public void Generate(float h)
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        int vertexCountX = vertexCount;
        int vertexCountZ = vertexCount;

        Vector3[] vertices = new Vector3[vertexCountX * vertexCountZ];
        int[] triangles = new int[(vertexCountX - 1) * (vertexCountZ - 1) * 6];
        Vector2[] uvs = new Vector2[vertexCountX * vertexCountZ]; // Ajoutez un tableau de coordonnées UV

        // Remplissez le tableau des vertices et des hauteurs ici
        for (int z = 0; z < vertexCountZ; z++)
        {
            for (int x = 0; x < vertexCountX; x++)
            {
                float xPos = x / (float)(vertexCountX - 1);
                float zPos = z / (float)(vertexCountZ - 1);
                float y = h;
                int index = z * vertexCountX + x;
                vertices[index] = new Vector3(xPos, y, zPos);

                // Assurez-vous que les coordonnées UV correspondent à la texture
                uvs[index] = new Vector2(xPos, zPos);
            }
        }

        int triangleIndex = 0;
        for (int z = 0; z < vertexCountZ - 1; z++)
        {
            for (int x = 0; x < vertexCountX - 1; x++)
            {
                int a = z * vertexCountX + x;
                int b = a + 1;
                int c = (z + 1) * vertexCountX + x;
                int d = c + 1;

                triangles[triangleIndex++] = a;
                triangles[triangleIndex++] = c;
                triangles[triangleIndex++] = b;

                triangles[triangleIndex++] = b;
                triangles[triangleIndex++] = c;
                triangles[triangleIndex++] = d;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs; // Appliquez les coordonnées UV au mesh
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        // Assurez-vous que le matériau est correctement appliqué au Mesh Renderer
        if (material != null)
        {
            GetComponent<MeshRenderer>().material = material;
        }
    }
}

