using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]


public class MyMesh : MonoBehaviour
{
    Mesh mesh;
    void Start()
    {
        mesh = GetComponent<Mesh>();
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        this.mesh = new Mesh();
        meshFilter.mesh = this.mesh;

        //vertex, triangles and uvs generation....


        this.mesh.RecalculateNormals();
        this.mesh.RecalculateBounds();
        this.mesh.Optimize();
    }

}