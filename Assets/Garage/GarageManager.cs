using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GarageManager : MonoBehaviour
{
    public static GarageManager Singleton;
    
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private Transform inventoryContentTransform;

    [SerializeField] private TextMeshProUGUI scrapCountText;

    private void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }

        Singleton = this;
    }

    private void Start()
    {
        BuildInventory();
    }

    private void BuildInventory()
    {
        for (int i = 0; i < PlayerScrapAndBagManager.Singleton.thingsStoredInBag.Count; i++)
        {
            GameObject objectToGive = Instantiate(inventoryItemPrefab, inventoryContentTransform);
            objectToGive.GetComponent<InventoryItemUIScript>().Init(PlayerScrapAndBagManager.Singleton.thingsStoredInBag[i]);
        }
    }

    public void ScrapAllMarkedForScrapping()
    {
        GameObject[] allInventoryItems = GameObject.FindGameObjectsWithTag("InventoryUIItem");

        for (int i = allInventoryItems.Length - 1; i >= 0; i--)
        {
            InventoryItemUIScript invenItem = allInventoryItems[i].GetComponent<InventoryItemUIScript>();
            if (invenItem.markedForScrapping)
            {
                PlayerScrapAndBagManager.Singleton.TurnItemIntoScrap(invenItem.refToTheClass);
                Destroy(invenItem.gameObject); //so we kill it so it no longer exists
            }
        }
    }

    private void Update()
    {
        scrapCountText.text = "Scrap Count: " + PlayerScrapAndBagManager.Singleton.ScrapCount;
    }

    public void ReBuildInventory()
    {
        Transform[] allChildren = inventoryContentTransform.GetComponentsInChildren<Transform>();
        for (int i = allChildren.Length - 1; i >= 1; i--)
        {
            Destroy(allChildren[i].gameObject);
        }
        BuildInventory();
    }

    public void ExitGarage()
    {
        SceneManager.LoadScene("Spicy Garage");
    }
}
