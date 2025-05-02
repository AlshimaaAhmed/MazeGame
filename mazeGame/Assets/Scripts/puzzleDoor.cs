using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleDoorTrigger : MonoBehaviour
{
    public Transform door;
    public Vector3 targetPosition;
    public float doorSpeed = 2f;

    public Transform player;
    public float playerMoveDistance = 2f;
    public float playerMoveSpeed = 1.5f;

    public float sinkDepth = 2f;
    public float sinkSpeed = 1f;

    public AudioSource sinkSound;

    public string nextSceneName = "RiddleScene";

    private bool doorOpened = false;
    private bool playerMoving = false;
    private bool playerSinking = false;

    private Vector3 playerTargetPosition;
    private Vector3 sinkTargetPosition;
    public Animator playerAnimator;

    public string doorName;  // «”„ «·»«» «··Ì Â‰Õÿ ›ÌÂ «·”ﬂ—Ì» 

    // «·’Ê— «·Œ«’… »ﬂ· »«»
    public Sprite QuestionSprite;
    public Sprite BackgroundSprite;


    void OnTriggerEnter(Collider other)
    {
        if (!doorOpened && other.CompareTag("Player"))
        {
            doorOpened = true;

            if (doorName == "catDoorPos")
            {
                DatatoBeShared.Question = "What did ancient Egyptians use to write?";
                DatatoBeShared.Answer1 = "Wood";
                DatatoBeShared.Answer2 = "Papyrus";
                DatatoBeShared.Answer3 = "StonesStones";
                DatatoBeShared.Answer4 = "Leather";
                DatatoBeShared.Questionimg = QuestionSprite;
                DatatoBeShared.Backgroundimg = BackgroundSprite;
            }
            else if (doorName == "crowDoorPos")
            {
                DatatoBeShared.Question = "Which god was associated with wisdom?";
                DatatoBeShared.Answer1 = "Ra";
                DatatoBeShared.Answer2 = "Thoth";
                DatatoBeShared.Answer3 = "Horus";
                DatatoBeShared.Answer4 = "Seth";
                DatatoBeShared.Questionimg = QuestionSprite;
                DatatoBeShared.Backgroundimg = BackgroundSprite;
            }
            else if (doorName == "lotusDoorPos")
            {
                DatatoBeShared.Question = "What structure guarded the pyramids?";
                DatatoBeShared.Answer1 = "Scarab";
                DatatoBeShared.Answer2 = "Obelisk";
                DatatoBeShared.Answer3 = "Statue of Ra";
                DatatoBeShared.Answer4 = "The Sphinx";
                DatatoBeShared.Questionimg = QuestionSprite;
                DatatoBeShared.Backgroundimg = BackgroundSprite;
            }

            // »œ¡ Õ—ﬂ… «·»«»
            door.position = Vector3.MoveTowards(door.position, targetPosition, doorSpeed * Time.deltaTime);
        }
    }

    void Update()
    {
        if (doorOpened)
        {
            door.position = Vector3.MoveTowards(door.position, targetPosition, doorSpeed * Time.deltaTime);

            if (Vector3.Distance(door.position, targetPosition) < 0.01f)
            {
                doorOpened = false;
                MovePlayerForward();
            }
        }

        if (playerMoving)
        {
            player.position = Vector3.MoveTowards(player.position, playerTargetPosition, playerMoveSpeed * Time.deltaTime);

            if (Vector3.Distance(player.position, playerTargetPosition) < 0.01f)
            {
                playerMoving = false;
                StartPlayerSink();
            }
        }

        if (playerSinking)
        {
            player.position = Vector3.MoveTowards(player.position, sinkTargetPosition, sinkSpeed * Time.deltaTime);

            if (Vector3.Distance(player.position, sinkTargetPosition) < 0.01f)
            {
                playerSinking = false;
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    void MovePlayerForward()
    {
        playerTargetPosition = player.position + player.forward * playerMoveDistance;
        playerMoving = true;
    }

    void StartPlayerSink()
    {
        sinkTargetPosition = player.position + new Vector3(0, -sinkDepth, 0);
        playerSinking = true;

        if (sinkSound != null)
        {
            sinkSound.Play();
        }

        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Sink");
        }
    }
}
