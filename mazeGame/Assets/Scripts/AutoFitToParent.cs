using UnityEngine;

public class AutoFitToParent : MonoBehaviour
{
    public Vector3 modelOriginalSize = new Vector3(8.8f, 4.9f, 8f); 
    public float scaleFactor = 0.1f; 

    void Start()
    {
        Transform parent = transform.parent;
        if (parent == null) return;

        Vector3 parentScale = parent.localScale;

        Vector3 newScale = new Vector3(
            parentScale.x / modelOriginalSize.x * scaleFactor,
            parentScale.y / modelOriginalSize.y * scaleFactor,
            parentScale.z / modelOriginalSize.z * scaleFactor
        );

        transform.localScale = newScale;
    }
}
