using UnityEngine;
using UnityEngine.SceneManagement;

public class RiddleAnswerChecker : MonoBehaviour
{
    public void OnAnswerClicked(string selectedAnswer)
    {
        if (PlayerManager.Instance == null)
        {
            Debug.LogError("❌ PlayerManager is null!");
            return;
        }

        string returnSceneName = "Level " + PlayerManager.Instance.playerData.currentLevel.ToString();

        if (selectedAnswer == DatatoBeShared.CorrectAnswer)
        {
            PlayerManager.Instance.AddKey(); // إضافة مفتاح
            Debug.Log("✅ Correct Answer - Key Added");
            SceneManager.LoadScene(returnSceneName);
        }
        else
        {
            PlayerManager.Instance.TakeDamage(1); // خسارة قلب
            Debug.Log("❌ Wrong Answer - Heart Removed");
        }
    }
}
