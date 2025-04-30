using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUpManager : MonoBehaviour
{
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene"); 
    }

    public void OpenShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void OpenNextLevel()
    {
       SceneManager.LoadScene("Level 2");
    }
}
