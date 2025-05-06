using UnityEngine;
using UnityEngine.SceneManagement;

public class RiddleAnswerChecker : MonoBehaviour
{
    public string returnSceneName = "Level 1";

    public void OnAnswerClicked(string selectedAnswer)
    {
        if (selectedAnswer == DatatoBeShared.CorrectAnswer)
        {
            if (PlayerManager.Instance != null)
            {
                PlayerManager.Instance.AddKey(); // إضافة مفتاح
            }

            Debug.Log("✅ Correct Answer - Key Added");
            SceneManager.LoadScene(returnSceneName);
        }
        else
        {
            if (PlayerManager.Instance != null)
            {
                PlayerManager.Instance.TakeDamage(1); // خسارة قلب
            }

            Debug.Log("❌ Wrong Answer - Heart Removed");
        }
    }
}
