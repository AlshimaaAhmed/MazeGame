using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public PlayerData playerData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            playerData = PlayerData.LoadData();

            Debug.Log($"🔑 Loaded Keys: {playerData.keys}");
            Debug.Log($"💰 Loaded Coins: {playerData.coins}");
            Debug.Log($"❤️ Loaded Lives: {playerData.lives}");
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void TakeDamage(int damage)
    {
        playerData.lives -= damage;
        if (playerData.lives < 0)
        {
            playerData.lives = 0;
        }

        PlayerData.SaveData(playerData);

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateLives(playerData.lives);
        }
    }

    public void AddKey()
    {
        playerData.keys++;
        PlayerData.SaveData(playerData);

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateKeys(playerData.keys);
        }
    }

    public void AddCoins(int amount)
    {
        playerData.coins += amount;
        PlayerData.SaveData(playerData);

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateCoins(playerData.coins);
        }
        if (UIManagerShop.Instance != null)
        {
            UIManagerShop.Instance.UpdateCoinsShop(playerData.coins);
        }
    }
    public bool SpendCoins(int amount)
    {
        if (playerData.coins >= amount)
        {
            playerData.coins -= amount;
            PlayerData.SaveData(playerData);
            UIManager.Instance?.UpdateCoins(playerData.coins);
            UIManagerShop.Instance.UpdateCoinsShop(playerData.coins);

            return true;
        }
        return false;
    }

    public void AddLife()
    {
        playerData.lives++;
        PlayerData.SaveData(playerData);
        UIManager.Instance?.UpdateLives(playerData.lives);
    }

    public void AddTime(float extraSeconds)
    {
        UIManager.Instance?.AddTime(extraSeconds);
    }

    public void AddTimeBoost(int amount)
    {
        playerData.timeBoosts += amount;
        PlayerData.SaveData(playerData);
        UIManager.Instance?.UpdateTimeBoostButton(playerData.timeBoosts);
        UIManagerShop.Instance?.UpdateTimeBoostButtonShop(playerData.timeBoosts);

    }

    public bool UseTimeBoost()
    {
        if (playerData.timeBoosts > 0)
        {
            playerData.timeBoosts--;
            PlayerData.SaveData(playerData);
            UIManager.Instance?.UpdateTimeBoostButton(playerData.timeBoosts);
            UIManagerShop.Instance?.UpdateTimeBoostButtonShop(playerData.timeBoosts);
            AddTime(30); 
            return true;
        }
        return false;
    }

}
