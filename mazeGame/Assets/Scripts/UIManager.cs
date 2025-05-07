using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Image[] heartIcons;
    public Image[] keyIcons;
    public Sprite activeKey;
    public Sprite inactiveKey;

    private int currentHearts;
    private int currentKeys = 0;
    private int currentCoins = 0;
    private int currenttboost = 0;

    public float startTime = 60f;
    private float currentTime;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI coinsText;
    public Button timeBoostButton;
    public TextMeshProUGUI boostButtonText;
    public Button shopButton;

    public string gameOverScene = "GameOver";
    public string levelUpScene = "Leveling up";
    Color customColor;

    public CanvasGroup canvasGroup;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameOver" || scene.name == "MainMenuScene" || scene.name == "Leveling up" || scene.name == "Shop")
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);

            if (canvasGroup != null)
            {
                if (scene.name == "RiddleScene")
                {
                    canvasGroup.blocksRaycasts = false;
                }
                else
                {
                    canvasGroup.blocksRaycasts = true;
                }
            }
        }
    }

    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        shopButton.interactable = true;
        shopButton.onClick.AddListener(OpenShop);

        if (PlayerManager.Instance != null)
        {
            currentHearts = PlayerManager.Instance.playerData.lives;
            currentKeys = PlayerManager.Instance.playerData.keys;
            currentCoins = PlayerManager.Instance.playerData.coins;
            currenttboost = PlayerManager.Instance.playerData.timeBoosts;
            UpdateUI();
        }

        currentTime = startTime;
    }

    void Update()
    {
        if (currentHearts > 0)
        {
            UpdateTimer();
        }
    }

    public void UpdateLives(int lives)
    {
        currentHearts = lives;

        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].enabled = (i < currentHearts);
        }

        if (currentHearts <= 0)
        {
            GameOver();
        }
    }

    public void UpdateKeys(int keys)
    {
        currentKeys = keys;

        for (int i = 0; i < keyIcons.Length; i++)
        {
            keyIcons[i].sprite = (i < currentKeys) ? activeKey : inactiveKey;
        }
    }

    public void UpdateCoins(int coins)
    {
        currentCoins = coins;
        if (coinsText != null)
        {
            coinsText.text = coins.ToString();
        }
    }

    private void UpdateUI()
    {
        UpdateLives(currentHearts);
        UpdateKeys(currentKeys);
        UpdateCoins(currentCoins);
        UpdateTimeBoostButton(currenttboost);
    }

    private void UpdateTimer()
    {
        if (ColorUtility.TryParseHtmlString("#5C421C", out customColor))
        {
            timerText.color = customColor;
        }
        currentTime -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (currentTime <= 10f)
        {
            timerText.color = Color.red;
        }

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            GameOver();
        }
    }

    public void UpdateTimeBoostButton(int boostCount)
    {
        if (boostButtonText != null)
        {
            boostButtonText.text = boostCount.ToString();
        }

        if (timeBoostButton != null)
        {
            timeBoostButton.interactable = boostCount > 0;
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene(gameOverScene);
    }

    public void AddTime(float seconds)
    {
        startTime += seconds;
        currentTime += seconds;
    }

    public void OpenShop()
    {
        DatatoBeShared.LastScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("shop");
    }

    public void UseTimeBoost()
    {
        if (!PlayerManager.Instance.UseTimeBoost())
        {
            Debug.Log("❌ No time boosts available.");
        }
    }
}
