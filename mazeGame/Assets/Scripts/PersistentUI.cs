using UnityEngine;
using UnityEngine.SceneManagement; // ������ �������

public class PersistentUI : MonoBehaviour
{
    // ��� ������ ����� ����� ��� ����� ������
    void Awake()
    {
        // ������ �� ��� ������ ������
        if (SceneManager.GetActiveScene().name != "Shop")
        {
            // ������ ��� ������ ��� ������� ��������
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // ��� ��� ������ �� ���� ���� �������� �����
            Destroy(gameObject);
        }
    }
}
