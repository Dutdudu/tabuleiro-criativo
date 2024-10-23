using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{
    public static int cont = 0;
    public GameObject piecePrefab;
    public Transform[] spawnPoints;

    // Prefabs das formas que serão instanciadas
    public GameObject circlePrefab;
    public GameObject squarePrefab;
    public GameObject trianglePrefab;
    public GameObject starPrefab;
    public GameObject pentagonPrefab;
    public GameObject hexagonPrefab;

    void Start()
    {
        // Carrega as quantidades salvas usando PlayerPrefs
        int circleCount = PlayerPrefs.GetInt("CircleCount", 0);
        int squareCount = PlayerPrefs.GetInt("SquareCount", 0);
        int triangleCount = PlayerPrefs.GetInt("TriangleCount", 0);
        int starCount = PlayerPrefs.GetInt("StarCount", 0);
        int pentagonCount = PlayerPrefs.GetInt("PentagonCount", 0);
        int hexagonCount = PlayerPrefs.GetInt("HexagonCount", 0);

        // Pega as cores salvas dos dropdowns
        Color circleColor = GetColor(PlayerPrefs.GetInt("CircleColor", 0));
        Color squareColor = GetColor(PlayerPrefs.GetInt("SquareColor", 0));
        Color triangleColor = GetColor(PlayerPrefs.GetInt("TriangleColor", 0));
        Color starColor = GetColor(PlayerPrefs.GetInt("StarColor", 0));
        Color pentagonColor = GetColor(PlayerPrefs.GetInt("PentagonColor", 0));
        Color hexagonColor = GetColor(PlayerPrefs.GetInt("HexagonColor", 0));

        // Chama o método para instanciar as formas conforme as quantidades e cores carregadas
        SpawnShapes(circleCount, circlePrefab, circleColor);
        SpawnShapes(squareCount, squarePrefab, squareColor);
        SpawnShapes(triangleCount, trianglePrefab, triangleColor);
        SpawnShapes(starCount, starPrefab, starColor);
        SpawnShapes(pentagonCount, pentagonPrefab, pentagonColor);
        SpawnShapes(hexagonCount, hexagonPrefab, hexagonColor);
    }

    // Método para instanciar a quantidade de formas desejadas
    void SpawnShapes(int count, GameObject prefab, Color color)
    {
        for (int i = 0; i < count; i++)
        {
            // Verifica se o índice é maior do que o número de spawnPoints
            if (i >= spawnPoints.Length)
            {
                Debug.LogWarning("Número de objetos a serem instanciados é maior do que os pontos de spawn disponíveis.");
                break; // Interrompe o loop se não houver pontos de spawn suficientes
            }

            // Obtém a posição de spawn
            Vector2 spawnPosition = spawnPoints[cont].position;

            // Instancia o objeto na posição de spawn
            GameObject newShape = Instantiate(prefab, spawnPosition, Quaternion.identity);
            ChangeColor(newShape, color);
            // Opcional: Coloca o objeto instanciado sob um Transform pai (por exemplo, o próprio Canvas)
            newShape.transform.SetParent(transform);
            cont++;
        }
    }

    // Método para trocar a cor de um objeto
    void ChangeColor(GameObject obj, Color color)
    {
        // Para um objeto 2D com SpriteRenderer
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
    }

    // Método para pegar a cor de acordo com o índice do dropdown
    Color GetColor(int colorIndex)
    {
        switch (colorIndex)
        {
            case 1: return Color.blue;
            case 2: return Color.yellow;
            case 3: return Color.red;
            // Adicione mais cores conforme necessário
            default: return Color.white;
        }
    }
}
