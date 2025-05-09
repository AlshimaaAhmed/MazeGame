using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PuzzleDoorTrigger : MonoBehaviour
{
    public Transform door;
    private Vector3 targetPosition;
    public float doorSpeed = 2f;

    public Transform player;
    public float playerMoveDistance = 2f;
    public float playerMoveSpeed = 1.5f;

    public float sinkDepth = 2f;
    public float sinkSpeed = 1f;

    public AudioSource sinkSound;
    public AudioSource doorOpenSound;

    public string nextSceneName = "RiddleScene";

    private bool doorOpened = false;
    private bool playerMoving = false;
    private bool playerSinking = false;

    private Vector3 playerTargetPosition;
    private Vector3 sinkTargetPosition;
    public Animator playerAnimator;

    private string doorName;

    public Sprite QuestionSprite;
    public Sprite BackgroundSprite;

    [System.Serializable]
    public class QuestionData
    {
        public string question;
        public string[] answers;
        public string correctAnswer;
    }

    [System.Serializable]
    public class DoorQuestionEntry
    {
        public string doorName;
        public QuestionData questionData;
    }

    [System.Serializable]
    public class DoorQuestions
    {
        public List<DoorQuestionEntry> doors;
    }

    private List<DoorQuestionEntry> questionList;
    private CharacterController controller;

    private PlayerMove playerMovementScript;

    void Start()
    {
        controller = player.GetComponent<CharacterController>();
        playerMovementScript = player.GetComponent<PlayerMove>();

        targetPosition = new Vector3(door.position.x, door.position.y - 20f, door.position.z);

        if (door != null)
        {
            doorName = door.gameObject.name;
            Debug.Log("Door name set to: " + doorName);

            if (DatatoBeShared.EnteredDoors.Contains(doorName))
            {
                GetComponent<Collider>().enabled = false;
                this.enabled = false;
                door.position = new Vector3(door.position.x, door.position.y - 20f, door.position.z);
            }
        }
        else
        {
            Debug.LogWarning("Door reference is not assigned!");
        }

        LoadQuestions();
    }

    void LoadQuestions()
    {
        TextAsset jsonText = Resources.Load<TextAsset>("questions");
        if (jsonText != null)
        {
            string json = jsonText.text;
            DoorQuestions doorQuestions = JsonUtility.FromJson<DoorQuestions>(json);
            questionList = doorQuestions.doors;
        }
        else
        {
            Debug.LogError("JSON file not found in Resources!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!doorOpened && other.CompareTag("Player"))
        {
            doorOpened = true;

            DatatoBeShared.EnteredDoors.Add(doorName);
            DatatoBeShared.ReturnPosition = player.position;
            DatatoBeShared.ReturnScene = SceneManager.GetActiveScene().name;

            if (doorOpenSound != null)
                doorOpenSound.Play();

            if (questionList != null)
            {
                foreach (var entry in questionList)
                {
                    if (entry.doorName == doorName)
                    {
                        QuestionData q = entry.questionData;
                        DatatoBeShared.Question = q.question;
                        DatatoBeShared.Answer1 = q.answers[0];
                        DatatoBeShared.Answer2 = q.answers[1];
                        DatatoBeShared.Answer3 = q.answers[2];
                        DatatoBeShared.Answer4 = q.answers[3];
                        DatatoBeShared.CorrectAnswer = q.correctAnswer;
                        DatatoBeShared.Questionimg = QuestionSprite;
                        DatatoBeShared.Backgroundimg = BackgroundSprite;
                        break;
                    }
                }
            }

            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = false;
                playerMovementScript.StopImmediately();
            }

            GetComponent<Collider>().enabled = false;
        }
    }

    void Update()
    {
        if (doorOpened)
        {
            door.position = Vector3.MoveTowards(door.position, targetPosition, doorSpeed * Time.deltaTime);

            if (Vector3.Distance(door.position, targetPosition) < 0.01f)
            {
                door.position = targetPosition;
                doorOpened = false;

                MovePlayerForward();
            }
        }

        if (playerMoving)
        {
            Vector3 direction = (playerTargetPosition - player.position).normalized;
            controller.Move(direction * playerMoveSpeed * Time.deltaTime);

            if (Vector3.Distance(player.position, playerTargetPosition) < 0.05f)
            {
                playerMoving = false;
               
                StartPlayerSink();
            }
        }

        if (playerSinking)
        {
          
            Vector3 direction = (sinkTargetPosition - player.position).normalized;
            player.position += direction * sinkSpeed * Time.deltaTime;

            if (Vector3.Distance(player.position, sinkTargetPosition) < 0.05f)
            {
                playerSinking = false;

                if (playerAnimator != null)
                    playerAnimator.SetTrigger("idle");

                // Save the player's return position and the scene name for later
           

                // Transition to the next scene
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

        controller.enabled = false;

        if (sinkSound != null)
            sinkSound.Play();

        if (playerAnimator != null)
            playerAnimator.SetTrigger("Sink");
    }
}
