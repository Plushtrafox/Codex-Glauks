using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class InvertirNormales : MonoBehaviour
{





    void Start()
    {
        SkinnedMeshRenderer skinnedRenderer = GetComponent<SkinnedMeshRenderer>();
        Mesh mesh = skinnedRenderer.sharedMesh;

        // 1. Invertir normales
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        mesh.normals = normals;

        // 2. Invertir tangentes (si usas normal maps)
        Vector4[] tangents = mesh.tangents;
        for (int i = 0; i < tangents.Length; i++)
        {
            tangents[i].w = -tangents[i].w; // Solo la componente W
        }
        mesh.tangents = tangents;

        // 3. Invertir el orden de triángulos
        int[] triangles = mesh.triangles;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            // Swap de índices
            (triangles[i], triangles[i + 2]) = (triangles[i + 2], triangles[i]);
        }
        mesh.triangles = triangles;

        // 4. Actualizar el mesh
        mesh.RecalculateBounds();
        mesh.UploadMeshData(false); // MarkNoLongerReadable=false para mantener acceso
    }




}
