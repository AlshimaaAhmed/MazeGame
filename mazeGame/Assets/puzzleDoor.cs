using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleDoorTrigger : MonoBehaviour
{
    public Transform door;          // ���� ����� ��� �� ��� Inspector
    public Vector3 targetPosition;  // ����� ���� ����� �� �����
    public float speed = 2f;
    public string nextSceneName = "RiddleScene";

    private bool shouldMove = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ����� �� �������� ��� tag = Player
        {
            shouldMove = true;
        }
    }

    void Update()
    {
        if (shouldMove)
        {
            // ��� ����� ���� ��������
            door.position = Vector3.MoveTowards(door.position, targetPosition, speed * Time.deltaTime);
            StartCoroutine(WaitAndLoadScene());
        }
    }

    System.Collections.IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(1f); // ����� ����� ����� ��� ���� �����
        SceneManager.LoadScene(nextSceneName);
    }
}
