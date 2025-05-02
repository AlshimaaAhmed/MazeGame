using UnityEngine;
using UnityEngine.UI;

public class FadeInEffect : MonoBehaviour
{
    public float fadeDuration = 1f; // ��� �����

    private Image fadeImage;
    private float fadeAlpha = 1f;

    void Start()
    {
        fadeImage = GetComponent<Image>();
        StartCoroutine(FadeIn());
    }

    System.Collections.IEnumerator FadeIn()
    {
        while (fadeAlpha > 0f)
        {
            fadeAlpha -= Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, fadeAlpha); // ���� + ����
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 0); // ���� ��� ���� ������
        gameObject.SetActive(false); // ��� ������ ��� �� ����
    }
}
