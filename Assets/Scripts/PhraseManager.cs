using UnityEngine;

public class PhraseManager : MonoBehaviour
{
    public static PhraseManager Instance { get; private set; }
    public string Phrase { get; set; }

    private void Awake()
    {
        // Garante que só exista uma instância de PhraseManager e que ele persista entre cenas
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
