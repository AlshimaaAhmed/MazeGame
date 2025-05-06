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
    }
}
