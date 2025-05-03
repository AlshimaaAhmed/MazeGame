using UnityEngine;
using UnityEngine.SceneManagement;

public class RiddleAnswerChecker : MonoBehaviour
{
    public string returnSceneName = "Level 1";

    public void OnAnswerClicked(string selectedAnswer)
    {
        if (selectedAnswer == DatatoBeShared.CorrectAnswer)
        {
            if (UIManager.Instance != null)
            {
                UIManager.Instance.AddKey();
            }

            Debug.Log("✅ Correct Answer - Key Added");
            SceneManager.LoadScene(returnSceneName);

        }
        else
        {
            if (UIManager.Instance != null)
            {
                UIManager.Instance.TakeDamage(1);
            }

            Debug.Log("❌ Wrong Answer - Heart Removed");
        }

    }
}
