using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Transform door;          // «”Õ» «·»«» Â‰« „‰ «·‹ Inspector
    public Vector3 targetPosition;  // «·Ê÷⁄ «··Ì ÂÌ‰“· ·Â «·»«»
    public float speed = 2f;

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
        }
    }
}
