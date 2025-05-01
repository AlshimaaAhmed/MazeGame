using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Transform door;          // ���� ����� ��� �� ��� Inspector
    public Vector3 targetPosition;  // ����� ���� ����� �� �����
    public float speed = 2f;

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
        }
    }
}
