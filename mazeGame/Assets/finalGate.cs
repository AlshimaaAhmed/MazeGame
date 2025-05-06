using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalGate : MonoBehaviour
{
    public string nextSceneName = "Leveling up";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int currentKeys = PlayerManager.Instance.playerData.keys;

            if (currentKeys == 3)
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.Log("You need 3 keys to enter!");
            }
        }
    }
}