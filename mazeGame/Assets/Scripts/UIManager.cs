using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Elements")]
    public Image[] heartIcons;
    private int currentHearts;

    [Header("Key UI")]
    public Image[] keyIcons;
    public Sprite activeKey;   // «·„› «Õ «·„› ÊÕ
    public Sprite inactiveKey; // «·„› «Õ «·√”Êœ
    private int currentKeys = 0;

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
    }

    void Start()
    {
        currentHearts = heartIcons.Length;
        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].gameObject.SetActive(true);
        }

        UpdateKeyIcons();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) TakeDamage(1);
        if (Input.GetKeyDown(KeyCode.K)) AddKey();
    }

    public void TakeDamage(int damage)
    {
        currentHearts -= damage;
        if (currentHearts < 0) currentHearts = 0;
        UpdateHearts();
    }

    void UpdateHearts()
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].gameObject.SetActive(i < currentHearts);
        }
    }

    public void AddKey()
    {
        if (currentKeys < keyIcons.Length)
        {
            keyIcons[currentKeys].sprite = activeKey;
            currentKeys++;
        }

        UpdateKeyIcons();
    }

    public void ResetKeys()
    {
        currentKeys = 0;
        UpdateKeyIcons();
    }

    private void UpdateKeyIcons()
    {
        for (int i = 0; i < keyIcons.Length; i++)
        {
            keyIcons[i].sprite = i < currentKeys ? activeKey : inactiveKey;
        }
    }
}
