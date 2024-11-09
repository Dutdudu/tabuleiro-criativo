using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class ShapeSpawner : MonoBehaviourPun
{
    public Transform[] spawnPoints;
    public GameObject circlePrefab;
    public GameObject squarePrefab;
    public GameObject trianglePrefab;
    public GameObject starPrefab;
    public GameObject pentagonPrefab;
    public GameObject hexagonPrefab;

    private Dictionary<string, int> shapeCounts = new Dictionary<string, int>();
    private Dictionary<string, Color> shapeColors = new Dictionary<string, Color>();

    void Start()
    {
        // Only the Master Client will send data to others
        if (PhotonNetwork.IsMasterClient)
        {
            LoadSettings();
            Debug.Log("CircleCount: " + shapeCounts["Circle"]);
            Debug.Log("CircleColor: " + shapeColors["Circle"]);
            photonView.RPC("SpawnShapes", RpcTarget.AllBuffered);
        }
    }

    void LoadSettings()
    {
        // Load quantities from the previous scene
        shapeCounts["Circle"] = PlayerPrefs.GetInt("CircleCount", 0);
        shapeCounts["Square"] = PlayerPrefs.GetInt("SquareCount", 0);
        shapeCounts["Triangle"] = PlayerPrefs.GetInt("TriangleCount", 0);
        shapeCounts["Star"] = PlayerPrefs.GetInt("StarCount", 0);
        shapeCounts["Pentagon"] = PlayerPrefs.GetInt("PentagonCount", 0);
        shapeCounts["Hexagon"] = PlayerPrefs.GetInt("HexagonCount", 0);

        // Load colors from previous scene
        shapeColors["Circle"] = GetColor(PlayerPrefs.GetInt("CircleColor", 0));
        shapeColors["Square"] = GetColor(PlayerPrefs.GetInt("SquareColor", 0));
        shapeColors["Triangle"] = GetColor(PlayerPrefs.GetInt("TriangleColor", 0));
        shapeColors["Star"] = GetColor(PlayerPrefs.GetInt("StarColor", 0));
        shapeColors["Pentagon"] = GetColor(PlayerPrefs.GetInt("PentagonColor", 0));
        shapeColors["Hexagon"] = GetColor(PlayerPrefs.GetInt("HexagonColor", 0));
    }

    [PunRPC]
    void SpawnShapes() {
        int spawnIndex = 0;
        foreach (var shape in shapeCounts.Keys)
        {
            GameObject prefab = GetPrefab(shape);
            Color color = shapeColors[shape];
            int count = shapeCounts[shape];
            for (int i = 0; i < count; i++)
            {
                if (spawnIndex >= spawnPoints.Length)
                {
                    Debug.LogWarning("Not enough spawn points for all shapes.");
                    return;
                }

                Vector2 spawnPosition = spawnPoints[spawnIndex].position;
                GameObject newShape = PhotonNetwork.Instantiate(prefab.name, spawnPosition, Quaternion.identity);
                SetShapeColor(newShape, color);

                spawnIndex++;
            }
        }
    }

    GameObject GetPrefab(string shape)
    {
        switch (shape)
        {
            case "Circle": return circlePrefab;
            case "Square": return squarePrefab;
            case "Triangle": return trianglePrefab;
            case "Star": return starPrefab;
            case "Pentagon": return pentagonPrefab;
            case "Hexagon": return hexagonPrefab;
            default: return null;
        }
    }

    void SetShapeColor(GameObject obj, Color color)
    {
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
    }

    Color GetColor(int colorIndex)
    {
        switch (colorIndex)
        {
            case 1: return Color.blue;
            case 2: return Color.yellow;
            case 3: return Color.red;
            default: return Color.white;
        }
    }
}
