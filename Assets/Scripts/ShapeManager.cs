using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using Unity.VisualScripting;

public class ShapeManager : MonoBehaviourPunCallbacks {
    public int circleCount = 0;
    public int squareCount = 0;
    public int triangleCount = 0;
    public int starCount = 0;
    public int pentagonCount = 0;
    public int hexagonCount = 0;

    public SpriteRenderer circleRenderer ;
    public SpriteRenderer squareRenderer ;
    public SpriteRenderer triangleRenderer;
    public SpriteRenderer starRenderer;
    public SpriteRenderer pentagonRenderer;
    public SpriteRenderer hexagonRenderer;

    public TMP_Text circleText;
    public TMP_Text squareText;
    public TMP_Text triangleText;
    public TMP_Text starText;
    public TMP_Text pentagonText;
    public TMP_Text hexagonText;
    
    // Dropdowns para selecionar as cores
    public TMP_Dropdown circleColorDropdown;
    public TMP_Dropdown squareColorDropdown;
    public TMP_Dropdown triangleColorDropdown;
    public TMP_Dropdown starColorDropdown;
    public TMP_Dropdown pentagonColorDropdown;
    public TMP_Dropdown hexagonColorDropdown;

    public Color[] colors = new Color[] {
        Color.white,  // Branco
        Color.blue,   // Azul
        Color.yellow, // Amarelo
        Color.red     // Vermelho
    };
    void Start()
    {
        UpdateUI();
        //LoadColors();
        SetDropdownListeners();

        // If we're the master client, sync initial state to others
        if (PhotonNetwork.IsMasterClient) {
            SyncInitialState();
        }
    }

    private void SyncInitialState() {
        if (!PhotonNetwork.IsConnected) return;

        photonView.RPC("ReceiveInitialState", RpcTarget.Others, 
            circleCount, squareCount, triangleCount, 
            starCount, pentagonCount, hexagonCount);
    }

    [PunRPC]
    private void ReceiveInitialState(int circle, int square, int triangle,  int star, int pentagon, int hexagon) {
        circleCount = circle;
        squareCount = square;
        triangleCount = triangle;
        starCount = star;
        pentagonCount = pentagon;
        hexagonCount = hexagon;
        
        UpdateUI();
    }


    void SetDropdownListeners() {
        circleColorDropdown.onValueChanged.AddListener((int index) => OnColorChanged("CircleColor", index));
        squareColorDropdown.onValueChanged.AddListener((int index) => OnColorChanged("SquareColor", index));
        triangleColorDropdown.onValueChanged.AddListener((int index) => OnColorChanged("TriangleColor", index));
        starColorDropdown.onValueChanged.AddListener((int index) => OnColorChanged("StarColor", index));
        pentagonColorDropdown.onValueChanged.AddListener((int index) => OnColorChanged("PentagonColor", index));
        hexagonColorDropdown.onValueChanged.AddListener((int index) => OnColorChanged("HexagonColor", index));
    }

    public void OnColorChanged(string shape, int index)
    {
        Color selectedColor = colors[index];
        SetShapeColor(shape, selectedColor);

        if (PhotonNetwork.IsConnected) {
            photonView.RPC("SyncShapeColor", RpcTarget.Others, shape, index);
        }
        else {
            SaveColorsLocally();
        }
    }

    [PunRPC]
    void SyncShapeColor(string shape, int colorIndex) {
        SetShapeColor(shape, colors[colorIndex]);
    }


    void SetShapeColor(string shape, Color color) {
        switch (shape) {
            case "CircleColor": circleRenderer.color = color; break;
            case "SquareColor": squareRenderer.color = color; break;
            case "TriangleColor": triangleRenderer.color = color; break;
            case "StarColor": starRenderer.color = color; break;
            case "PentagonColor": pentagonRenderer.color = color; break;
            case "HexagonColor": hexagonRenderer.color = color; break;
        }
    }

    public void ChangeShapeCount(string shape, int delta, bool isNetworked = false)
    {
        if (!PhotonNetwork.IsConnected)
        {
            // Handle offline mode
            UpdateShapeCountLocally(shape, delta);
            UpdateUI();
            return;
        }

        // If this is a network message we received, just apply it
        if (isNetworked)
        {
            UpdateShapeCountLocally(shape, delta);
            UpdateUI();
            return;
        }

        // This is a local change - update locally and send to others
        UpdateShapeCountLocally(shape, delta);
        UpdateUI();
        photonView.RPC("SyncShapeCount", RpcTarget.Others, shape, delta);
    }

    private void UpdateShapeCountLocally(string shape, int delta) {
        // Add debug logging to track local updates
        Debug.Log($"Updating locally: Shape={shape}, Delta={delta}");
        
        switch (shape) {
            case "Circle": 
                circleCount = Mathf.Max(0, circleCount + delta);
                Debug.Log($"New circle count: {circleCount}");
                break;
            case "Square": 
                squareCount = Mathf.Max(0, squareCount + delta);
                Debug.Log($"New square count: {squareCount}");
                break;
            case "Triangle": 
                triangleCount = Mathf.Max(0, triangleCount + delta);
                Debug.Log($"New triangle count: {triangleCount}");
                break;
            case "Star": 
                starCount = Mathf.Max(0, starCount + delta);
                Debug.Log($"New star count: {starCount}");
                break;
            case "Pentagon": 
                pentagonCount = Mathf.Max(0, pentagonCount + delta);
                Debug.Log($"New pentagon count: {pentagonCount}");
                break;
            case "Hexagon": 
                hexagonCount = Mathf.Max(0, hexagonCount + delta);
                Debug.Log($"New hexagon count: {hexagonCount}");
                break;
            default: 
                Debug.LogWarning("Unknown shape: " + shape); 
                break;
        }
    }

    [PunRPC]
    void SyncShapeCount(string shape, int delta) {
        // Add debug logging to track RPC calls
        Debug.Log($"Received RPC SyncShapeCount: Shape={shape}, Delta={delta}");
        UpdateShapeCountLocally(shape, delta);
        UpdateUI();
    }
   

    public void IncreaseShape(string shape) {
        ChangeShapeCount(shape, 1);
    }

    public void DecreaseShape(string shape) {
        ChangeShapeCount(shape, -1);
    }


    void UpdateUI() {
        circleText.text = "Circulo: " + circleCount;
        squareText.text = "Quadrado: " + squareCount;
        triangleText.text = "Triângulo: " + triangleCount;
        starText.text = "Estrela: " + starCount;
        pentagonText.text = "Pentágono: " + pentagonCount;
        hexagonText.text = "Hexágono: " + hexagonCount;
    }

    public void SaveQuantities() {
        PlayerPrefs.SetInt("CircleCount", circleCount);
        PlayerPrefs.SetInt("SquareCount", squareCount);
        PlayerPrefs.SetInt("TriangleCount", triangleCount);
        PlayerPrefs.SetInt("StarCount", starCount);
        PlayerPrefs.SetInt("PentagonCount", pentagonCount);
        PlayerPrefs.SetInt("HexagonCount", hexagonCount);

        PlayerPrefs.Save(); // Salva as mudanças nos PlayerPrefs
}

    void SaveColorsLocally() {
        PlayerPrefs.SetInt("CircleColor", circleColorDropdown.value);
        PlayerPrefs.SetInt("SquareColor", squareColorDropdown.value);
        PlayerPrefs.SetInt("TriangleColor", triangleColorDropdown.value);
        PlayerPrefs.SetInt("StarColor", starColorDropdown.value);
        PlayerPrefs.SetInt("PentagonColor", pentagonColorDropdown.value);
        PlayerPrefs.SetInt("HexagonColor", hexagonColorDropdown.value);
        
        PlayerPrefs.Save();
    }

    /*
    void LoadColors() {
        circleColorDropdown.value = PlayerPrefs.GetInt("CircleColor", 0);
        squareColorDropdown.value = PlayerPrefs.GetInt("SquareColor", 0);
        triangleColorDropdown.value = PlayerPrefs.GetInt("TriangleColor", 0);
        starColorDropdown.value = PlayerPrefs.GetInt("StarColor", 0);
        pentagonColorDropdown.value = PlayerPrefs.GetInt("PentagonColor", 0);
        hexagonColorDropdown.value = PlayerPrefs.GetInt("HexagonColor", 0);

        // Atualizar a cor de cada sprite ao carregar
        SetShapeColor("CircleColor", colors[circleColorDropdown.value]);
        SetShapeColor("SquareColor", colors[squareColorDropdown.value]);
        SetShapeColor("Triangle", colors[triangleColorDropdown.value]);
        SetShapeColor("StarColor", colors[starColorDropdown.value]);
        SetShapeColor("PentagonColor", colors[pentagonColorDropdown.value]);
        SetShapeColor("HexagonColor", colors[hexagonColorDropdown.value]);
    }
    */

    public void SaveDataAndChangeScene() {
        SaveQuantities(); // Salva as quantidades selecionadas
        SaveColorsLocally();     // Salva as cores selecionadas
    }


    public void FinishAndLoadNextScene() {
        if (PhotonNetwork.IsMasterClient) {
            // Somente o clinte master pode confirmar a mudacanca de cena
            PhotonNetwork.LoadLevel("Gameplay");
        }
    } 
}
