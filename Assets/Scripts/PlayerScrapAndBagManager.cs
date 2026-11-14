using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScrapAndBagManager : MonoBehaviour
{
    public static PlayerScrapAndBagManager Singleton;

    [Header("Win Condition Settings")] 
    [SerializeField] private int winScoreThreshold;
    
    private List<Pickupable> thingsStoredInBag = new();
    
    private int scrapCount;
    public int ScrapCount => scrapCount;

    public void Awake()
    {
        if(Singleton != null)
        {
            Destroy(gameObject);
            return;
        }

        Singleton = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PutThingInBag(Pickupable thingToPutInBag)
    {
        thingsStoredInBag.Add(thingToPutInBag);
        thingToPutInBag.gameObject.SetActive(false);
        thingToPutInBag.transform.SetParent(transform);
    }

    public void TurnItemIntoScrap(Pickupable thingToScrap, bool halfCost = false)
    {
        scrapCount += thingToScrap.ScrapValue / (halfCost ? 2 : 1);
        if (thingsStoredInBag.Contains(thingToScrap)) thingsStoredInBag.Remove(thingToScrap);
        
        Destroy(thingToScrap.gameObject);
    }
}
