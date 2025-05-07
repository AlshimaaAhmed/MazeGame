using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Transform door;                  
    public Vector3 doorOpenPosition;       
    public float speed = 2f;

    public Transform player;               

    private bool shouldMoveDoor = false;
    private bool doorOpened = false;
   

    public AudioSource doorOpenSound;       
    public AudioSource fallSound;           

    public Animator playerAnimator;         
    public string fallAnimationTrigger = "fall"; 
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldMoveDoor = true;

            if (doorOpenSound != null)
                doorOpenSound.Play();

            if (playerAnimator != null)
                playerAnimator.SetTrigger("Sink");

            if (fallSound != null)
                fallSound.Play();

            PlayerManager.Instance.TakeDamage(1);
        }
    }

    void Update()
    {
        if (shouldMoveDoor && !doorOpened)
        {
            door.position = Vector3.MoveTowards(door.position, doorOpenPosition, speed * Time.deltaTime);

            if (Vector3.Distance(door.position, doorOpenPosition) < 0.01f)
            {
                doorOpened = true;

            }
        }

    }
}
