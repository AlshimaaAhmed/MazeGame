using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleDoorTrigger : MonoBehaviour
{
    public Transform door;          // «”Õ» «·»«» Â‰« „‰ «·‹ Inspector
    public Vector3 targetPosition;  // «·Ê÷⁄ «··Ì ÂÌ‰“· ·Â «·»«»
    public float speed = 2f;
    public string nextSceneName = "RiddleScene";

    private bool shouldMove = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // « √ﬂœ ≈‰ «·ﬂ«—ﬂ — ·ÌÂ tag = Player
        {
            shouldMove = true;
        }
    }

    void Update()
    {
        if (shouldMove)
        {
            // Õ—ﬂ «·»«» · Õ   œ—ÌÃÌ«
            door.position = Vector3.MoveTowards(door.position, targetPosition, speed * Time.deltaTime);
            StartCoroutine(WaitAndLoadScene());
        }
    }

    System.Collections.IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(1f); // «” ‰Ï À«‰Ì… ﬂ«„·… »⁄œ Õ—ﬂ… «·»«»
        SceneManager.LoadScene(nextSceneName);
    }
}
