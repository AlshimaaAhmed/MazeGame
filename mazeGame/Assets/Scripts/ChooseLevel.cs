using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    public void Level_One_Open()
    {
        SceneManager.LoadScene("level 1");
    }

    public void Level_Two_Open()
    {
        SceneManager.LoadScene("level 2");
    }

    public void Level_Three_Open()
    {
        SceneManager.LoadScene("level 3");
    }
}
