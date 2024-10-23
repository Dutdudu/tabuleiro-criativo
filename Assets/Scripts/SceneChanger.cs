using UnityEngine;
using UnityEngine.SceneManagement;  // Necessário para gerenciar cenas

public class SceneChanger : MonoBehaviour
{
    // Método para mudar para uma cena pelo nome
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Método para mudar para uma cena pelo índice
    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}