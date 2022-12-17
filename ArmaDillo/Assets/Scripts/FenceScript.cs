using UnityEngine;

public class FenceGenerator : MonoBehaviour
{
    // The game object to generate the fence around.
    public GameObject target;

    // The material to use for the fence.
    public Material material;

    // The width of the fence.
    public float width = 1.0f;

    // The height of the fence.
    public float height = 2.0f;

    // The number of fence segments to generate.
    public int segments = 16;

    // The radius of the fence.
    private float radius;

    void Start()
    {
        // Calculate the radius of the fence by getting the bounding
        // box of the target game object and using its x size.
        var bounds = target.GetComponent<Renderer>().bounds;
        radius = bounds.extents.x;

        // Generate the fence mesh.
        var mesh = GenerateFenceMesh();

        // Create a game object for the fence.
        var fence = new GameObject("Fence");

        // Attach the mesh to the fence game object.
        fence.AddComponent<MeshFilter>().mesh = mesh;
        fence.AddComponent<MeshRenderer>().material = material;

        // Position the fence game object around the target game object.
        fence.transform.position = target.transform.position;
        fence.transform.rotation = Quaternion.identity;
    }
    Mesh GenerateFenceMesh()
    {
        // Create a new mesh.
        var mesh = new Mesh();

        // Generate the vertices of the fence.
        var vertices = new Vector3[segments * 2];
        for (int i = 0; i < segments; i++)
        {
            // Calculate the angle of the current segment.
            var angle = Mathf.Deg2Rad * (360.0f / segments) * i;

            // Calculate the x and y coordinates of the vertices.
            var x = Mathf.Sin(angle) * radius;
            var y = Mathf.Cos(angle) * radius;

            // Set the vertices.
            vertices[i * 2] = new Vector3(x, 0.0f, y);
            vertices[i * 2 + 1] = new Vector3(x, height, y);
        }

        // Set the vertices of the mesh.
        mesh.vertices = vertices;

        // Generate the triangles of the mesh.
        var triangles = new int[segments * 6];
        for (int i = 0; i < segments; i++)
        {
            // Set the triangles.
            triangles[i * 6] = i * 2;
            triangles[i * 6 + 1] = i * 2 + 1;
            triangles[i * 6 + 2] = (i * 2 + 3) % (segments * 2);
            triangles[i * 6 + 3] = i * 2;
            triangles[i * 6 + 4] = (i * 2 + 3) % (segments * 2);
            triangles[i * 6 + 5] = (i * 2 + 2) % (segments * 2);
        }

        // Set the triangles of the mesh.
        mesh.triangles = triangles;

        // Return the generated mesh.
        return mesh;
    }
}