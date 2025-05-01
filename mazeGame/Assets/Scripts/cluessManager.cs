using UnityEngine;
using TMPro;

public class WallLookTrigger : MonoBehaviour
{
    public Transform playerCamera;
    public float triggerDistance = 1f;
    public GameObject cluePanel;
    public TMP_Text clueText;

    void Start()
    {
        cluePanel.SetActive(false);
    }

    void Update()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, triggerDistance))
        {
            string objectName = hit.collider.gameObject.name;
            Debug.Log("Hit: " + objectName);

            if (objectName == "Cleopatra1")
            {
                cluePanel.SetActive(true);
                clueText.text = "Cats were sacred in ancient Egypt.";
            }
            else if (objectName == "Cleopatra2")
            {
                cluePanel.SetActive(true);
                clueText.text = "The Lotus blooms from the mud.";
            }
            else if (objectName == "Cleopatra3")
            {
                cluePanel.SetActive(true);
                clueText.text = "Crows are omens in many cultures.";
            }
            else
            {
                cluePanel.SetActive(false);
            }
        }
        else
        {
            cluePanel.SetActive(false);
        }
    }
}