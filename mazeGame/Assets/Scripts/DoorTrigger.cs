using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PositionData
{
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public class DoorPositionEntry
{
    public string doorName;
    public PositionData position;
}

[System.Serializable]
public class DoorPositionsWrapper
{
    public List<DoorPositionEntry> doors;
}

public class DoorTrigger : MonoBehaviour
{
    public Transform door;
    public float speed = 2f;

    public Transform player;
    public AudioSource doorOpenSound;
    public AudioSource fallSound;
    public AudioSource minerSound;

    public Animator playerAnimator;
    public string fallAnimationTrigger = "Sink";
    public string idleAnimationTrigger = "Idle";

    public Transform miner;
    public Transform minerMoveTarget;
    public Animator minerAnimator;
    public string minerWalkTrigger = "Stab";
    public string minerIdleTrigger = "Idle";

    public GameObject blackScreen;

    private Vector3 doorOpenPosition;
    private Vector3 originalDoorPosition;
    private Vector3 originalMinerPosition;

    private bool shouldMoveDoor = false;
    private bool doorReturned = false;

    private List<DoorPositionEntry> doorPositionList;

    private CharacterController playerController;
    private PlayerMove playerMovementScript;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (door == null)
            door = transform.Find("Door");

        if (miner == null)
            miner = transform.Find("Miner");

        if (minerAnimator == null && miner != null)
            minerAnimator = miner.GetComponent<Animator>();

        if (playerAnimator == null && player != null)
            playerAnimator = player.GetComponent<Animator>();

        if (player != null)
        {
            playerController = player.GetComponent<CharacterController>();
            playerMovementScript = player.GetComponent<PlayerMove>();
        }

        if (door != null)
        {
            originalDoorPosition = door.position;
            doorOpenPosition = new Vector3(door.position.x, door.position.y - 20f, door.position.z);
        }

        if (miner != null)
            originalMinerPosition = miner.position;

        LoadDoorPositions();
    }

    void LoadDoorPositions()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("positions");

        if (jsonFile != null)
        {
            string json = jsonFile.text;
            DoorPositionsWrapper wrapper = JsonUtility.FromJson<DoorPositionsWrapper>(json);
            doorPositionList = wrapper.doors;
        }
        else
        {
            Debug.LogError("positions.json not found in Resources!");
            doorPositionList = new List<DoorPositionEntry>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!shouldMoveDoor)
            {
                shouldMoveDoor = true;

                if (playerMovementScript != null)
                {
                    playerMovementScript.StopImmediately(); // إيقاف فوري
                    playerMovementScript.enabled = false;
                }

                if (doorOpenSound != null) doorOpenSound.Play();
                StartCoroutine(StartScene());
            }

            PlayerManager.Instance?.TakeDamage(1);
        }
    }

    void Update()
    {
        if (shouldMoveDoor && !doorReturned && door != null)
        {
            door.position = Vector3.MoveTowards(door.position, doorOpenPosition, speed * Time.deltaTime);
        }

        if (player != null && door != null)
        {
            float distanceToDoor = Vector3.Distance(player.position, door.position);

            if (distanceToDoor < 2f)
            {
                if (playerMovementScript != null)
                    playerMovementScript.enabled = false;
            }
            else
            {
                if (!shouldMoveDoor && playerMovementScript != null)
                    playerMovementScript.enabled = true;
            }
        }

        if (doorReturned && door != null)
        {
            shouldMoveDoor = false;
            doorReturned = false;
        }
    }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(1f);

        if (minerAnimator != null) minerAnimator.SetTrigger(minerWalkTrigger);
        if (minerSound != null) minerSound.Play();

        if (playerAnimator != null) playerAnimator.SetTrigger(fallAnimationTrigger);
        if (fallSound != null) fallSound.Play();

        yield return new WaitForSeconds(0.8f);

        if (minerAnimator != null) minerAnimator.SetTrigger(minerIdleTrigger);

        if (blackScreen != null) blackScreen.SetActive(true);

        if (playerAnimator != null) playerAnimator.SetTrigger(idleAnimationTrigger);

        MovePlayerImmediately();

        if (!doorReturned && door != null)
        {
            door.position = originalDoorPosition;
            doorReturned = true;
        }

        while (miner != null && Vector3.Distance(miner.position, originalMinerPosition) > 0.1f)
        {
            miner.position = Vector3.MoveTowards(miner.position, originalMinerPosition, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        if (blackScreen != null)
            blackScreen.SetActive(false);

        if (playerMovementScript != null)
            playerMovementScript.enabled = true;

        shouldMoveDoor = false;
        doorReturned = false;
    }

    void MovePlayerImmediately()
    {
        if (door == null || player == null || playerController == null) return;

        string doorName = door.name;

        foreach (var entry in doorPositionList)
        {
            if (entry.doorName == doorName)
            {
                PositionData targetPos = entry.position;
                Vector3 targetPosition = new Vector3(targetPos.x, targetPos.y, targetPos.z);

                playerController.enabled = false;
                player.position = targetPosition;
                playerController.enabled = true;

                Debug.Log($"Moving player to position: {targetPosition}");
                return;
            }
        }

        Debug.LogWarning("No target position found for door: " + doorName);
    }
}
