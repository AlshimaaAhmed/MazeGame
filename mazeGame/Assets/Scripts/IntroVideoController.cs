using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Button skipButton;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
        skipButton.onClick.AddListener(SkipVideo);
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        LoadMainMenu();
    }

    void SkipVideo()
    {
        videoPlayer.Stop();
        LoadMainMenu();
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene"); 
        skipButton.gameObject.SetActive(false);
    }

}
