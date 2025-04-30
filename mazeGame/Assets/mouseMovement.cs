using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    float yRotation = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;

        // ��� ������ ���� ���� �����
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
