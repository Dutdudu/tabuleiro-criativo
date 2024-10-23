using UnityEngine;

public class Square : MonoBehaviour
{
    public int index; // Índice do quadrado

    void Start()
    {
        SquareState squareState = GameManager.Instance.squareState;
        Renderer renderer = GetComponent<Renderer>();

        // Clonando o material para evitar compartilhamento entre os quadrados
        renderer.material = new Material(renderer.material);

        if (squareState != null && squareState.squareColors.Length > index)
        {
            renderer.material.color = squareState.squareColors[index] != null ? 
                                       squareState.squareColors[index] : 
                                       Color.white;
        }
        else
        {
            renderer.material.color = Color.white;
        }
    }

    public void SetColor(Color color)
    {
        SquareState squareState = GameManager.Instance.squareState;
        Renderer renderer = GetComponent<Renderer>();

        if (squareState != null && squareState.squareColors.Length > index)
        {
            squareState.squareColors[index] = color;
            renderer.material.color = color;
        }
    }
}