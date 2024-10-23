using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class ShapeManager : MonoBehaviour
{
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

   public Color[] colors = new Color[]
{
    Color.white,  // Branco
    Color.blue,   // Azul
    Color.yellow, // Amarelo
    Color.red     // Vermelho
};

    void Start()
    {
        UpdateUI();
        LoadColors();
       
        circleColorDropdown.onValueChanged.AddListener(delegate { ChangeCircleColor(circleColorDropdown.value); });
        squareColorDropdown.onValueChanged.AddListener(delegate { ChangeSquareColor(squareColorDropdown.value); });
        triangleColorDropdown.onValueChanged.AddListener(delegate { ChangeTriangleColor(triangleColorDropdown.value); });
        starColorDropdown.onValueChanged.AddListener(delegate { ChangeStarColor(starColorDropdown.value); });
        pentagonColorDropdown.onValueChanged.AddListener(delegate { ChangePentagonColor(pentagonColorDropdown.value); });
        hexagonColorDropdown.onValueChanged.AddListener(delegate { ChangeHexagonColor(hexagonColorDropdown.value); });
    }
    void ChangeCircleColor(int index)
    {
        circleRenderer.color = colors[index];
        Debug.Log("Selected index: " + index);
    }

    void ChangeSquareColor(int index)
    {
        squareRenderer.color = colors[index];
    }

    void ChangeTriangleColor(int index)
    {
        triangleRenderer.color = colors[index];
    }

    void ChangeStarColor(int index)
    {
        starRenderer.color = colors[index];
    }

    void ChangePentagonColor(int index)
    {
        pentagonRenderer.color = colors[index];
    }

    void ChangeHexagonColor(int index)
    {
        hexagonRenderer.color = colors[index];
    }

    public void IncreaseCircle()
    {
        circleCount++;
        UpdateUI();
    }
    public void DecreaseCircle()
    {
        if(circleCount!=0)
            circleCount--;
        UpdateUI();
    }

    public void IncreaseSquare()
    {
        
        squareCount++;
       UpdateUI();
    }
    public void DecreaseSquare()
    {
        if(squareCount!=0)
            squareCount--;
       UpdateUI();
    }

    public void IncreaseTriangle()
    {
        triangleCount++;
      UpdateUI();
    }
    public void DecreaseTriangle()
    {
        if(triangleCount!=0)
            triangleCount--;
      UpdateUI();
    }

    public void IncreaseStar()
    {
        starCount++;
       UpdateUI();
    }
    public void DecreaseStar()
    {
        if(starCount!=0)
            starCount--;
       UpdateUI();
    }

    public void IncreasePentagon()
    {
        pentagonCount++;
        UpdateUI();
    }
    public void DecreasePentagon()
    {
        if(pentagonCount!=0)
            pentagonCount--;
        UpdateUI();
    }

    public void IncreaseHexagon()
    {
        hexagonCount++;
        UpdateUI();
    }
    public void DecreaseHexagon()
    {
        if(hexagonCount!=0)
            hexagonCount--;
        UpdateUI();
    }

    void UpdateUI()
    {
        
        Debug.LogWarning("Chamado");
        circleText.text = "Circle: " + circleCount;
        squareText.text = "Square: " + squareCount;
        triangleText.text = "Triangle: " + triangleCount;
        starText.text = "Star: " + starCount;
        pentagonText.text = "Pentagon: " + pentagonCount;
        hexagonText.text = "Hexagon: " + hexagonCount;
    }
     public void SaveQuantities()
{
    Debug.LogWarning(circleCount);
    PlayerPrefs.SetInt("CircleCount", circleCount);
    PlayerPrefs.SetInt("SquareCount", squareCount);
    PlayerPrefs.SetInt("TriangleCount", triangleCount);
    PlayerPrefs.SetInt("StarCount", starCount);
    PlayerPrefs.SetInt("PentagonCount", pentagonCount);
    PlayerPrefs.SetInt("HexagonCount", hexagonCount);

    PlayerPrefs.Save(); // Salva as mudanças nos PlayerPrefs
}

    void SaveColors()
    {
        PlayerPrefs.SetInt("CircleColor", circleColorDropdown.value);
        PlayerPrefs.SetInt("SquareColor", squareColorDropdown.value);
        PlayerPrefs.SetInt("TriangleColor", triangleColorDropdown.value);
        PlayerPrefs.SetInt("StarColor", starColorDropdown.value);
        PlayerPrefs.SetInt("PentagonColor", pentagonColorDropdown.value);
        PlayerPrefs.SetInt("HexagonColor", hexagonColorDropdown.value);
        
        PlayerPrefs.Save();
    }

    void LoadColors()
    {
        circleColorDropdown.value = PlayerPrefs.GetInt("CircleColor", 0);
        squareColorDropdown.value = PlayerPrefs.GetInt("SquareColor", 0);
        triangleColorDropdown.value = PlayerPrefs.GetInt("TriangleColor", 0);
        starColorDropdown.value = PlayerPrefs.GetInt("StarColor", 0);
        pentagonColorDropdown.value = PlayerPrefs.GetInt("PentagonColor", 0);
        hexagonColorDropdown.value = PlayerPrefs.GetInt("HexagonColor", 0);

        // Atualizar a cor de cada sprite ao carregar
        ChangeCircleColor(circleColorDropdown.value);
        ChangeSquareColor(squareColorDropdown.value);
        ChangeTriangleColor(triangleColorDropdown.value);
        ChangeStarColor(starColorDropdown.value);
        ChangePentagonColor(pentagonColorDropdown.value);
        ChangeHexagonColor(hexagonColorDropdown.value);
    }

    public void SaveDataAndChangeScene()
    {
        SaveQuantities(); // Salva as quantidades selecionadas
        SaveColors();     // Salva as cores selecionadas
        // SceneManager.LoadScene("NextScene"); // Substitua "NextScene" pelo nome da próxima cena
    }
    
}
