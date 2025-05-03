using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("level 1");
    }

    public void OpenShop()
    {
        SceneManager.LoadScene("shop");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}