using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    public AudioSource footstepsAudio;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleAnimation();
        HandleFootsteps();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void HandleAnimation()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        bool isWalking = Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f;
        animator.SetBool("isWalking", isWalking);
    }

    void HandleFootsteps()
    {
        bool isMoving = controller.velocity.magnitude > 0.1f && controller.isGrounded;

        if (isMoving)
        {
            if (!footstepsAudio.isPlaying)
                footstepsAudio.Play();
        }
        else
        {
            if (footstepsAudio.isPlaying)
                footstepsAudio.Stop();
        }
    }
    public void StopImmediately()
    {
        controller.Move(Vector3.zero); // Êﬁ› «·Õ—ﬂ… ›Ê—Ì«
        if (animator != null)
            animator.SetBool("isWalking", false);

        if (footstepsAudio != null && footstepsAudio.isPlaying)
            footstepsAudio.Stop();
    }
}
// This script handles player movement, animation, and footstep sounds.
// It uses Unity's CharacterController for smooth movement and includes basic input handling.