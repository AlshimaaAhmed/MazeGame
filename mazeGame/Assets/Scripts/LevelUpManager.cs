using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelUpManager : MonoBehaviour
{
    public GameObject statueReward;
    public GameObject GoldenBox;

    void Start()
    {
        int currentLevel = PlayerManager.Instance.playerData.currentLevel;
        Debug.Log($"🎮 Current level: {currentLevel}");

        // Hide all rewards
        if (statueReward != null)
            statueReward.SetActive(false);

        if (GoldenBox != null)
            GoldenBox.SetActive(false);

        // Show appropriate reward
        if (currentLevel == 1)
        {
            ShowLevel1Reward();
        }
        else if (currentLevel == 2)
        {
            ShowLevel2Reward();
        }
        else if (currentLevel >= 3)
        {
            HandleLevel3Reward();
        }
    }

    private void ShowLevel1Reward()
    {
        Debug.Log("🏆 Attempting to show Level 1 reward");

        if (!HasCollectedReward("statue"))
        {
            CollectReward(); // Collect before showing

            if (statueReward != null)
            {
                statueReward.SetActive(true);
                RewardRotation rot = statueReward.GetComponent<RewardRotation>();
                if (rot != null)
                    rot.StartRotation();

                Debug.Log("✅ Statue reward activated");

                PlayerManager.Instance.playerData.currentLevel++;
                PlayerData.SaveData(PlayerManager.Instance.playerData);
                Debug.Log($"🔼 Level increased to {PlayerManager.Instance.playerData.currentLevel}");
            }
        }
        else
        {
            Debug.Log("ℹ️ Reward already collected, going to main menu");
            OpenMainMenu();
        }
    }

    private void ShowLevel2Reward()
    {
        Debug.Log("🏆 Attempting to show Level 2 reward");

        if (!HasCollectedReward("GoldenBox"))
        {
            CollectReward(); // Collect before showing

            if (GoldenBox != null)
            {
                GoldenBox.SetActive(true);
                Debug.Log("✅ Golden Box activated");
                RewardRotation rot = GoldenBox.GetComponent<RewardRotation>();
                if (rot != null)
                    rot.StartRotation();

                Debug.Log("✅ Statue reward activated");

                PlayerManager.Instance.playerData.currentLevel++;
                PlayerData.SaveData(PlayerManager.Instance.playerData);
                Debug.Log($"🔼 Level increased to {PlayerManager.Instance.playerData.currentLevel}");
            }
        }
        else
        {
            Debug.Log("ℹ️ Reward already collected, going to main menu");
            OpenMainMenu();
        }
    }

    private void HandleLevel3Reward()
    {
        Debug.Log("🏆 Attempting to handle Level 3 reward");

        if (!HasCollectedReward("Treasure"))
        {
            CollectReward(); // Collect before loading scene

            AddReward("Treasure");
            Debug.Log("🔄 Loading TreasureRoomScene");
            SceneManager.LoadScene("TreasureRoomScene");
        }
        else
        {
            Debug.Log("ℹ️ Level 3 reward already collected, going to main menu");
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    private bool HasCollectedReward(string rewardName)
    {
        if (PlayerManager.Instance.playerData.collectedRewards == null)
            return false;

        return PlayerManager.Instance.playerData.collectedRewards.Contains(rewardName);
    }

    private void AddReward(string rewardName)
    {
        if (!HasCollectedReward(rewardName))
        {
            PlayerManager.Instance.playerData.collectedRewards.Add(rewardName);
            PlayerData.SaveData(PlayerManager.Instance.playerData);
            Debug.Log($"✅ Added '{rewardName}' to collected rewards");
        }
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OpenShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void OpenNextLevel()
    {
        string nextLevelName = "Level " + PlayerManager.Instance.playerData.currentLevel;
        Debug.Log($"🔄 Navigating to scene: {nextLevelName}");
        SceneManager.LoadScene(nextLevelName);
    }

    public void OpenLevel(int levelNumber)
    {
        PlayerManager.Instance.playerData.currentLevel = levelNumber;
        Debug.Log($"📍 Current level set to {levelNumber}");

        PlayerData.SaveData(PlayerManager.Instance.playerData);

        string levelName = "Level " + levelNumber;
        Debug.Log($"🔄 Navigating to scene: {levelName}");
        SceneManager.LoadScene(levelName);
    }

    public void CollectReward()
    {
        Debug.Log("💰 Collecting reward...");

        int currentLevel = PlayerManager.Instance.playerData.currentLevel;

        if (currentLevel == 1 && !HasCollectedReward("statue"))
        {
            PlayerManager.Instance.AddCoins(50); // استخدم AddCoin الجاهزة
            AddReward("statue");

            if (statueReward != null)
            {
                statueReward.SetActive(false);
                Debug.Log("✅ Statue reward hidden");
            }
        }
        else if (currentLevel == 2 && !HasCollectedReward("GoldenBox"))
        {
            PlayerManager.Instance.AddCoins(200); // استخدم AddCoin الجاهزة
            AddReward("GoldenBox");

            if (GoldenBox != null)
            {
                GoldenBox.SetActive(false);
                Debug.Log("✅ Golden Box hidden");
            }
        }
        else if (currentLevel >= 3 && !HasCollectedReward("Treasure"))
        {
            PlayerManager.Instance.AddCoins(1_000_000); // استخدم AddCoin الجاهزة
            AddReward("Treasure");
            Debug.Log("💰 Added 1,000,000 coins for Level 3 reward");
        }

        PlayerData.SaveData(PlayerManager.Instance.playerData);
    }


    public void ResetAllData()
    {
        PlayerManager.Instance.playerData = new PlayerData();
        PlayerData.SaveData(PlayerManager.Instance.playerData);
        Debug.Log("🔄 All data has been reset");
    }
}
