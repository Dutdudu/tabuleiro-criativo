using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputScene : MonoBehaviour
{
    public InputField inputField;

    public void OnSubmit()
    {
        // Salva a frase digitada no PhraseManager
        PhraseManager.Instance.Phrase = inputField.text;
        
        // Muda para a segunda cena
        
    }
}
