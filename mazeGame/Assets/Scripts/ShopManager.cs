using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public class ShopItem
{
    public string name;
    public int cost;
    public string effect;
    // "heart", "key", "time30", "time60"
}

[System.Serializable]
public class ShopData
{
    public List<ShopItem> items;
}

public class ShopManager : MonoBehaviour
{
    private List<ShopItem> shopItems;
    public AudioSource purchaseAudioSource;


    private void Start()
    {
        LoadShopItems();
    }

    void LoadShopItems()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "shop_items.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ShopData data = JsonUtility.FromJson<ShopData>(json);
            shopItems = data.items;

            // Debug: Print all items
            foreach (var item in shopItems)
            {
                Debug.Log($"Item: {item.name}, Cost: {item.cost}, Effect: {item.effect}");
            }
        }
        else
        {
            Debug.LogError("shop_items.json not found!");
        }
    }

    private bool isProcessing = false;

    public void BuyItem(string effect)
    {
        if (isProcessing || shopItems == null) return;

        ShopItem item = shopItems.Find(i => i.effect == effect);
        if (item == null)
        {
            Debug.LogWarning("Item not found: " + effect);
            return;
        }

        isProcessing = true;


        if (PlayerManager.Instance.SpendCoins(item.cost))
        {
            if (purchaseAudioSource != null)
            {
                purchaseAudioSource.Play();
            }
            switch (item.effect)
            {
                case "heart":
                    PlayerManager.Instance.AddLife();
                    break;
                case "key":
                    PlayerManager.Instance.AddKey();
                    break;
                case "time30":
                    PlayerManager.Instance.AddTimeBoost(1);
                    break;
                case "time60":
                    PlayerManager.Instance.AddTimeBoost(2);
                    break;
            }

            Debug.Log($"{item.name} purchased!");
        }
        else
        {
            Debug.Log("Not enough coins to buy: " + item.name);
        }

        isProcessing = false;
    }



    public void BackToMenu()
    {
        SceneManager.LoadScene(DatatoBeShared.LastScene);
    }
}
