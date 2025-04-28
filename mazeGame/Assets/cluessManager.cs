using UnityEngine;

public class WallLookTrigger : MonoBehaviour
{
    public Transform playerCamera; // «·ﬂ«„Ì—« «··Ì » »’ „‰Â«
    public float triggerDistance = 5f; // «·„”«›… «·„ÿ·Ê»…
    public GameObject cleopatraModel; // „ÊœÌ· ﬂ·ÌÊ» —«
    public GameObject cluePanel; // «·»«‰· «··Ì ÂÌŸÂ—

    void Start()
    {
        cleopatraModel.SetActive(false);
        cluePanel.SetActive(false);
    }

    void Update()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, triggerDistance))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                cleopatraModel.SetActive(true);
                cluePanel.SetActive(true);
            }
        }
        else
        {
            cleopatraModel.SetActive(false);
            cluePanel.SetActive(false);
        }
    }
}
