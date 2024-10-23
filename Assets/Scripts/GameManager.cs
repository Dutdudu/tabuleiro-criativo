using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Instância estática do GameManager
    public SquareState squareState; // Referência ao ScriptableObject que armazena as cores dos quadrados

    private void Awake()
    {
        // Verifica se já existe uma instância do GameManager
        if (Instance == null)
        {
            Instance = this; // Define esta instância como a instância única
            DontDestroyOnLoad(gameObject); // Garante que o GameManager não seja destruído ao carregar uma nova cena
        }
        else
        {
            Destroy(gameObject); // Destroi qualquer nova instância criada, garantindo apenas uma instância ativa
        }
    }
}
