using UnityEngine;
using UnityEngine.SceneManagement; // áÅÏÇÑÉ ÇáãÔÇåÏ

public class PersistentUI : MonoBehaviour
{
    // åĞå ÇáÏÇáÉ ÊõäİĞ ÚäÏãÇ íÊã ÊÍãíá ÇáßÇÆä
    void Awake()
    {
        // ÇáÊÍŞŞ ãä ÇÓã ÇáãÔåÏ ÇáÍÇáí
        if (SceneManager.GetActiveScene().name != "Shop")
        {
            // ÇáÍİÇÙ Úáì ÇáßÇÆä ÚÈÑ ÇáãÔÇåÏ ÇáãÎÊáİÉ
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // ÅĞÇ ßÇä ÇáãÔåÏ åæ ÇáĞí ÊÑíÏ ÇÓÊËäÇÄå¡ ÇÍĞİå
            Destroy(gameObject);
        }
    }
}
