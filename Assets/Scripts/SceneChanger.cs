using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadYourScene(sceneName));
    }

    private IEnumerator LoadYourScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("DevScene");

        while (!asyncLoad.isDone)
        {
            // Puedes mostrar una barra de progreso aquí si quieres
            yield return null;
        }
    }
}