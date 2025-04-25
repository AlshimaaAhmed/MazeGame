using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Elements")]
    public Image[] heartIcons;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public Image[] keyIcons;
    public Sprite activeKey;
    public Sprite inactiveKey;

    public TMP_Text coinText;
    public TMP_Text timerText;
    public TMP_Text timeBoostsText;

    public GameObject gameOverPanel;

    private int maxHearts = 5;
    private int currentHearts;
    private int coins = 0;
    private int keys = 0;
    private int timeBoosts = 0;
    private float timer = 300f;

    private float refillTimer = 0f;
    private float refillInterval = 60f;

    [Header("Level Detection")]
    public bool isInLevel = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentHearts = maxHearts;
        UpdateHearts();
        UpdateCoins(coins);
        UpdateKeys(keys);
        UpdateTimer();
        UpdateTimeBoosts(timeBoosts);
    }

    void Update()
    {
        if (isInLevel && currentHearts > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                GameOver();
            }
            UpdateTimer();
        }

        if (!isInLevel && currentHearts < maxHearts)
        {
            refillTimer += Time.deltaTime;
            if (refillTimer >= refillInterval)
            {
                currentHearts++;
                if (currentHearts > maxHearts) currentHearts = maxHearts;
                refillTimer = 0f;
                UpdateHearts();
                Debug.Log("1 ❤️ Refilled!");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHearts -= damage;
        if (currentHearts < 0) currentHearts = 0;
        UpdateHearts();

        if (currentHearts == 0)
        {
            GameOver();
        }
    }

    public void AddHeart(int amount)
    {
        currentHearts += amount;
        if (currentHearts > maxHearts) currentHearts = maxHearts;
        UpdateHearts();
    }

    public void UpdateLives(int lives)
    {
        currentHearts = lives;
        if (currentHearts > maxHearts) currentHearts = maxHearts;
        UpdateHearts();
    }

    void UpdateHearts()
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].sprite = i < currentHearts ? fullHeart : emptyHeart;
        }
    }

    public void UpdateCoins(int amount)
    {
        coins = amount;
        if (coinText != null)
            coinText.text = coins.ToString();
    }

    public void UpdateKeys(int amount)
    {
        keys = amount;
        for (int i = 0; i < keyIcons.Length; i++)
        {
            keyIcons[i].sprite = i < keys ? activeKey : inactiveKey;
        }
    }

    public void UpdateTimeBoosts(int count)
    {
        timeBoosts = count;
        if (timeBoostsText != null)
            timeBoostsText.text = "⏱ x" + count.ToString();
    }

    void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
    
     public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void OpenShop()
    {
        SceneManager.LoadScene("Shop");
    }
    /*
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }*/


    void GameOver()
    {
        Debug.Log("Game Over!");
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UpdateAllUI(int newCoins, int newLives, int newKeys, int newBoosts)
    {
        UpdateCoins(newCoins);
        UpdateLives(newLives);
        UpdateKeys(newKeys);
        UpdateTimeBoosts(newBoosts);
    }
}
