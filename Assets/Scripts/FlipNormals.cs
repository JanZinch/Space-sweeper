using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FlipNormals : MonoBehaviour
{
    [SerializeField] private bool inverted = false;

    private void Start()
    {
        if (!inverted)
        {
            Mesh mesh = GetComponent<MeshFilter>().sharedMesh;

            Vector3[] normals = mesh.normals;
            for (int i = 0; i < normals.Length; i++)
            {
                normals[i] *= -1;
            }

            mesh.normals = normals;

            for (int i = 0; i < mesh.subMeshCount; i++)
            {
                int[] tris = mesh.GetTriangles(i);
                for (int j = 0; j < tris.Length; j += 3)
                {
                    //swap order of tri vertices
                    int temp = tris[j];
                    tris[j] = tris[j + 1];
                    tris[j + 1] = temp;
                }

                mesh.SetTriangles(tris, i);
            }

            inverted = true;
        }
    }
}
