using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClueData
{
    public string characterName;
    public string clue;
}

[System.Serializable]
public class ClueDataList
{
    public List<ClueData> clues;
}

public class ClueManager : MonoBehaviour
{
    public static ClueManager Instance;

    public TextAsset jsonFile; // «”Õ»Ì „·› clues.json Â‰« „‰ Inspector
    private Dictionary<string, string> clueDictionary;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadClues();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadClues()
    {
        clueDictionary = new Dictionary<string, string>();
        ClueDataList dataList = JsonUtility.FromJson<ClueDataList>("{\"clues\":" + jsonFile.text + "}");

        foreach (ClueData data in dataList.clues)
        {
            clueDictionary[data.characterName] = data.clue;
        }
    }

    public string GetClueForCharacter(string characterName)
    {
        if (clueDictionary.ContainsKey(characterName))
        {
            return clueDictionary[characterName];
        }
        return "·« ÌÊÃœ  ·„ÌÕ.";
    }
}
