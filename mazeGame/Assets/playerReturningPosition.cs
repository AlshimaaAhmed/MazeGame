using UnityEngine;

public class PlayerReturnPosition : MonoBehaviour
{
    void Start()
    {

        Debug.Log("DatatoBeShared.ReturnPosition: " + DatatoBeShared.ReturnPosition);

        // �� �� ���� ���� �����
        if (DatatoBeShared.ReturnPosition != Vector3.zero)
        {
            Debug.Log("playerPosition: " + DatatoBeShared.ReturnPosition);

            // ���� ������ ������ ������
            transform.position = DatatoBeShared.ReturnPosition;

            // ��� ������ ����� �� ����� ����� ������
            DatatoBeShared.ReturnPosition = Vector3.zero;
        }
    }
}
