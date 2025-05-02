using UnityEngine;
using UnityEngine.UI;
using TMPro;  // „Â„ ·Ê » ” Œœ„Ì TextMeshPro

public class RiddleSceneManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button optionButton1;
    public Button optionButton2;
    public Button optionButton3;
    public Button optionButton4;
    public Image questionImage;
    public Image backgroundImage;

    void Start()
    {
        // ﬁ—«¡… «·»Ì«‰«  „‰ PlayerPrefs
        string question = PlayerPrefs.GetString("Question");
        string option1 = PlayerPrefs.GetString("Option1");
        string option2 = PlayerPrefs.GetString("Option2");
        string option3 = PlayerPrefs.GetString("Option3");
        string option4 = PlayerPrefs.GetString("Option4");
        string questionImageName = PlayerPrefs.GetString("QuestionImage");
        string backgroundImageName = PlayerPrefs.GetString("BackgroundImage");

        // œÌ»«Ã ·· √ﬂœ „‰ «·»Ì«‰« 
        Debug.Log("Question: " + question);
        Debug.Log("Option1: " + option1);
        Debug.Log("Option2: " + option2);
        Debug.Log("Option3: " + option3);
        Debug.Log("Option4: " + option4);
        Debug.Log("QuestionImageName: " + questionImageName);
        Debug.Log("BackgroundImageName: " + backgroundImageName);

        //  ⁄ÌÌ‰ «·‰’Ê’ Ê«·’Ê—
        questionText.text = question;
        optionButton1.GetComponentInChildren<TextMeshProUGUI>().text = option1;
        optionButton2.GetComponentInChildren<TextMeshProUGUI>().text = option2;
        optionButton3.GetComponentInChildren<TextMeshProUGUI>().text = option3;
        optionButton4.GetComponentInChildren<TextMeshProUGUI>().text = option4;

        //  Õ„Ì· «·’Ê— „‰ Resources
        Sprite qImage = Resources.Load<Sprite>("Images/" + questionImageName);
       // Sprite bgImage = Resources.Load<Sprite>("Backgrounds/" + backgroundImageName);

       // if (qImage == null)
       // {
        //    Debug.LogError("Failed to load QuestionImage: " + questionImageName);
       // }
       // if (bgImage == null)
       // {
        //    Debug.LogError("Failed to load BackgroundImage: " + backgroundImageName);
       // }

        //questionImage.sprite = qImage;
        //backgroundImage.sprite = bgImage;
    }
}
