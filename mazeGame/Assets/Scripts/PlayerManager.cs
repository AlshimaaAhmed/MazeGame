using UnityEngine;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int coins = 0;
    public int lives = 0;
    public int keys = 0;
    public int timeBoosts = 0;
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public PlayerData playerData = new PlayerData();

    private string filePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        filePath = Path.Combine(Application.streamingAssetsPath, "player_data.json");
        LoadPlayerData();
    }

    public void SavePlayerData()
    {
        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(filePath, json);
    }

    public void LoadPlayerData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            playerData = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            SavePlayerData(); // create default
        }

        UIManager.Instance?.UpdateAllUI(playerData.coins, playerData.lives, playerData.keys, playerData.timeBoosts);
    }

    public bool SpendCoins(int amount)
    {
        if (playerData.coins >= amount)
        {
            playerData.coins -= amount;

            // احفظ بعد الخصم فقط
            SavePlayerData();

            // حدث الواجهة
            UIManager.Instance?.UpdateCoins(playerData.coins);

            return true;
        }

        Debug.Log("Not enough coins!");
        return false;
    }

    public void AddLife()
    {
        playerData.lives++;
        SavePlayerData();
        UIManager.Instance?.UpdateLives(playerData.lives);
    }

    public void AddKey()
    {
        playerData.keys++;
        SavePlayerData();
        UIManager.Instance?.UpdateKeys(playerData.keys);
    }

    public void AddTimeBoost()
    {
        playerData.timeBoosts++;
        SavePlayerData();
        UIManager.Instance?.UpdateTimeBoosts(playerData.timeBoosts);
    }
}
