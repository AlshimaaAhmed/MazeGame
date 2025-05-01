using UnityEngine;
using UnityEngine.SceneManagement;

public class RiddleLoader : MonoBehaviour
{
    public Transform playerCamera;
    public float triggerDistance = 2f;
    public string targetObjectName = "PuzzleDoor1";
    public string sceneToLoad = "RiddleScene";

    private bool hasLoaded = false;

    void Update()
    {
        if (hasLoaded || playerCamera == null) return;

        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, triggerDistance))
        {
            if (hit.collider.gameObject.name == targetObjectName)
            {
                hasLoaded = true;
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
