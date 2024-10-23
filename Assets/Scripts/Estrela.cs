using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class StarMesh : MonoBehaviour
{
    void Start()
    {
        CreateStarMesh();
    }

    void CreateStarMesh()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        // Define os vértices da estrela
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0, 1, 0),  // Ponto de cima
            new Vector3(0.2f, 0.2f, 0),
            new Vector3(1, 0.2f, 0),
            new Vector3(0.3f, -0.1f, 0),
            new Vector3(0.5f, -1, 0),
            new Vector3(0, -0.4f, 0),
            new Vector3(-0.5f, -1, 0),
            new Vector3(-0.3f, -0.1f, 0),
            new Vector3(-1, 0.2f, 0),
            new Vector3(-0.2f, 0.2f, 0),
        };

        // Define os triângulos que compõem a estrela
        int[] triangles = new int[]
        {
            0, 1, 9,
            1, 2, 4,
            1, 4, 3,
            4, 5, 6,
            4, 6, 8,
            6, 7, 9,
            7, 0, 9
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        // Adicionar uma cor ao material para visualizar melhor
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material = new Material(Shader.Find("Sprites/Default"));
        renderer.material.color = Color.yellow;
    }
}
