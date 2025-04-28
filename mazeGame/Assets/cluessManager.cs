using UnityEngine;

public class WallLookTrigger : MonoBehaviour
{
    public Transform playerCamera; // �������� ���� ���� ����
    public float triggerDistance = 5f; // ������� ��������
    public GameObject cleopatraModel; // ����� ��������
    public GameObject cluePanel; // ������ ���� �����

    void Start()
    {
        cleopatraModel.SetActive(false);
        cluePanel.SetActive(false);
    }

    void Update()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, triggerDistance))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                cleopatraModel.SetActive(true);
                cluePanel.SetActive(true);
            }
        }
        else
        {
            cleopatraModel.SetActive(false);
            cluePanel.SetActive(false);
        }
    }
}
