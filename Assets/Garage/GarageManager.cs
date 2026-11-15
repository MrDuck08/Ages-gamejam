using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GarageManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private Transform inventoryContentTransform;

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
    
    public void ExitGarage()
    {
        SceneManager.LoadScene("Spicy Garage");
    }
}
