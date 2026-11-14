using System;
using UnityEngine;

public class PlayerBagDeleter : MonoBehaviour
{
    private void Start()
    {
        if (PlayerScrapAndBagManager.Singleton != null)
        {
            Destroy(PlayerScrapAndBagManager.Singleton.gameObject);
            PlayerScrapAndBagManager.Singleton = null;
        }
    }
}
