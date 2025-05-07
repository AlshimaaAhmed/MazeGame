using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int currentLevel = 1;
    public int coins = 0;
    public int lives = 5;
    public int keys = 0;
    public int timeBoosts = 0;
    public List<string> collectedRewards = new List<string>();

    public static void SaveData(PlayerData playerData)
    {
        try
        {
            string json = JsonUtility.ToJson(playerData, true);
            string path = GetSavePath();
            File.WriteAllText(path, json);
            Debug.Log($"✅ Data saved successfully at: {path}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"❌ Error saving data: {e.Message}");
        }
    }

    public static PlayerData LoadData()
    {
        string path = GetSavePath();

        try
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);

                if (loadedData.collectedRewards == null)
                    loadedData.collectedRewards = new List<string>();
                else
                    loadedData.collectedRewards.RemoveAll(item => string.IsNullOrEmpty(item));

                return loadedData;
            }
            else
            {
                Debug.Log("⚠️ Data file not found, creating new data");
                PlayerData newData = new PlayerData();
                SaveData(newData);
                return newData;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"❌ Error loading data: {e.Message}");
            return new PlayerData();
        }
    }

    private static string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, "player_data.json");
    }
}
