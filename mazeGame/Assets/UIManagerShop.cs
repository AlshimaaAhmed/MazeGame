using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManagerShop : MonoBehaviour
{
    public static UIManagerShop Instance;
    private int currentCoins = 0;
    private int currenttboost = 0;

    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI boostButtonText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    IEnumerator Start()
    {
        yield return new WaitUntil(() => PlayerManager.Instance != null);

        currentCoins = PlayerManager.Instance.playerData.coins;
        currenttboost = PlayerManager.Instance.playerData.timeBoosts;
        UpdateUI();
    }
    void Update()
    {
        
    }

    public void UpdateCoinsShop(int coins)
    {
        currentCoins = coins;
        if (coinsText != null)
        {
            coinsText.text = coins.ToString();
        }
    }

    public void UpdateTimeBoostButtonShop(int boostCount)
    {
        if (boostButtonText != null)
        {
            boostButtonText.text = boostCount.ToString();
        }

    }


    private void UpdateUI()
    {
        Debug.Log($"[SHOP UI] Updating UI: Coins = {currentCoins}, Boosts = {currenttboost}");

        UpdateCoinsShop(currentCoins);
        UpdateTimeBoostButtonShop(currenttboost);
    }

}
