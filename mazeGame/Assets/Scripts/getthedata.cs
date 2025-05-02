using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class getthedata : MonoBehaviour
{
    [SerializeField] TMP_Text Question;
    [SerializeField] TMP_Text Answer1;
    [SerializeField] TMP_Text Answer2;
    [SerializeField] TMP_Text Answer3;
    [SerializeField] TMP_Text Answer4;

    [SerializeField] Image questionImage; // Image component for the panel (the one you want to change the sprite of)
    [SerializeField] Image backgroundImage;

    void Start()
    {
        // ⁄—÷ «·»Ì«‰«  «·Œ«’… »«·”ƒ«·
        Debug.Log("Question: " + DatatoBeShared.Question);

        if (Question != null)
        {
            Question.text = DatatoBeShared.Question;
        }

        if (Answer1 != null)
        {
            Answer1.text = DatatoBeShared.Answer1;
        }

        if (Answer2 != null)
        {
            Answer2.text = DatatoBeShared.Answer2;
        }

        if (Answer3 != null)
        {
            Answer3.text = DatatoBeShared.Answer3;
        }

        if (Answer4 != null)
        {
            Answer4.text = DatatoBeShared.Answer4;
        }

        //  €ÌÌ— ’Ê—… «·‹ questionImage («·‹ Panel)
        if (questionImage != null && DatatoBeShared.Questionimg != null)
        {
            questionImage.sprite = DatatoBeShared.Questionimg; //  €ÌÌ— «·’Ê—…
        }

        //  €ÌÌ— ’Ê—… «·Œ·›Ì…
        if (backgroundImage != null && DatatoBeShared.Backgroundimg != null)
        {
            backgroundImage.sprite = DatatoBeShared.Backgroundimg;
        }
    }
}
