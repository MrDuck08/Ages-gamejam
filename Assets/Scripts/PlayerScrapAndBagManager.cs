using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScrapAndBagManager : MonoBehaviour
{
    public static PlayerScrapAndBagManager Singleton;

    [Header("Win Condition Settings")] 
    [SerializeField] private int winScoreThreshold;

    private PickupableObjectInfoClass leftHandStoredWeapon;
    private PickupableObjectInfoClass rightHandStoredWeapon;
    
    public PickupableObjectInfoClass LeftHandStoredWeapon => leftHandStoredWeapon;
    public PickupableObjectInfoClass RightHandStoredWeapon => rightHandStoredWeapon;
    
    [SerializeField] private List<PickupableObjectInfoClass> thingsStoredInBag = new();
    
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
        float durability = 0f;

        switch (thingToPutInBag.Type)
        {
            case PickupableType.Weapon:
                durability = thingToPutInBag.GetComponent<WeaponBaseClass>().Durability;
                break;
            case PickupableType.Hull:
                durability = thingToPutInBag.GetComponent<BaseHull>().Durability;
                break;
            case PickupableType.Legs:
                durability = thingToPutInBag.GetComponent<BaseLegs>().Durability;
                break;
        }
        
        thingsStoredInBag.Add(new PickupableObjectInfoClass(durability, thingToPutInBag.prefabReference.prefabHolden, thingToPutInBag.ScrapValue));
        Destroy(thingToPutInBag.gameObject);
    }

    public void RefreshWeaponInfoObjects()
    {
        if (!PlayerWeaponHandler.Singleton.IsWeaponAFist(PlayerWeaponHandler.Singleton.LeftHandCurrentWeapon))
        {
            leftHandStoredWeapon = new PickupableObjectInfoClass(
                PlayerWeaponHandler.Singleton.LeftHandCurrentWeapon.Durability,
                PlayerWeaponHandler.Singleton.LeftHandCurrentWeapon.gameObject.GetComponent<Pickupable>()
                    .prefabReference.prefabHolden,
                PlayerWeaponHandler.Singleton.LeftHandCurrentWeapon.gameObject.GetComponent<Pickupable>().ScrapValue);
        }
        else
        {
            leftHandStoredWeapon = null;
        }
        
        if (!PlayerWeaponHandler.Singleton.IsWeaponAFist(PlayerWeaponHandler.Singleton.RightHandCurrentWeapon))
        {
            rightHandStoredWeapon = new PickupableObjectInfoClass(
                PlayerWeaponHandler.Singleton.RightHandCurrentWeapon.Durability,
                PlayerWeaponHandler.Singleton.RightHandCurrentWeapon.gameObject.GetComponent<Pickupable>()
                    .prefabReference.prefabHolden, PlayerWeaponHandler.Singleton.RightHandCurrentWeapon.gameObject.GetComponent<Pickupable>().ScrapValue);
        }
        else
        {
            rightHandStoredWeapon = null;
        }
    }
    
    public void TurnItemIntoScrap(Pickupable thingToScrap, bool halfCost = false)
    {
        //NOTE! Here we won't scrap it so we need to like uhhhh
        scrapCount += thingToScrap.ScrapValue / (halfCost ? 2 : 1);
        
        Destroy(thingToScrap.gameObject);
    }
    public void TurnItemIntoScrap(PickupableObjectInfoClass thingToScrap, bool halfCost = false)
    {
        //NOTE! Here we won't scrap it so we need to like uhhhh
        scrapCount += thingToScrap.scrapValue / (halfCost ? 2 : 1);
        thingsStoredInBag.Remove(thingToScrap);
    }
}
