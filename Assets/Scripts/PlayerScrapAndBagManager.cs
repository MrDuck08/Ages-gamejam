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
    
    private PickupableObjectInfoClass hullStored;
    private PickupableObjectInfoClass legsStored;
    
    public PickupableObjectInfoClass LeftHandStoredWeapon => leftHandStoredWeapon;
    public PickupableObjectInfoClass RightHandStoredWeapon => rightHandStoredWeapon;
    
    public PickupableObjectInfoClass HullStored => hullStored;
    public PickupableObjectInfoClass LegsStored => legsStored;
    
    [HideInInspector] public List<PickupableObjectInfoClass> thingsStoredInBag = new();
    
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
        
        thingsStoredInBag.Add(new PickupableObjectInfoClass(durability, thingToPutInBag.prefabReference.prefabHolden, thingToPutInBag.ScrapValue, thingToPutInBag.Type));
        Destroy(thingToPutInBag.gameObject);
    }

    public void PutThingInBag(int whatWereYou)
    {
        //0 == left hand
        //1 == right hand
        //2 == hull
        //3 == legs
        switch (whatWereYou)
        {
            case 0:
                if (leftHandStoredWeapon == null) return;
                thingsStoredInBag.Add(leftHandStoredWeapon);
                leftHandStoredWeapon = null;
                break;
            case 1:
                if (rightHandStoredWeapon == null) return;
                thingsStoredInBag.Add(rightHandStoredWeapon);
                rightHandStoredWeapon = null;
                break;
            case 2:
                if (hullStored == null) return;
                thingsStoredInBag.Add(hullStored);
                hullStored = null;
                break;
            case 3:
                if (legsStored == null) return;
                thingsStoredInBag.Add(legsStored);
                legsStored = null;
                break;
        }
    }

    public void RefreshWeaponInfoObjects()
    {
        if (!PlayerWeaponHandler.Singleton.IsWeaponAFist(PlayerWeaponHandler.Singleton.LeftHandCurrentWeapon))
        {
            leftHandStoredWeapon = new PickupableObjectInfoClass(
                PlayerWeaponHandler.Singleton.LeftHandCurrentWeapon.Durability,
                PlayerWeaponHandler.Singleton.LeftHandCurrentWeapon.gameObject.GetComponent<Pickupable>()
                    .prefabReference.prefabHolden,
                PlayerWeaponHandler.Singleton.LeftHandCurrentWeapon.gameObject.GetComponent<Pickupable>().ScrapValue, PickupableType.Weapon);
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
                    .prefabReference.prefabHolden, PlayerWeaponHandler.Singleton.RightHandCurrentWeapon.gameObject.GetComponent<Pickupable>().ScrapValue, PickupableType.Weapon);
        }
        else
        {
            rightHandStoredWeapon = null;
        }

        if (!PlayerMovement.Singleton.IsCurrentLegsBaseLegs())
        {
            legsStored = new PickupableObjectInfoClass(PlayerMovement.Singleton.Legs.Durability,
                PlayerMovement.Singleton.Legs.GetComponent<Pickupable>().prefabReference.prefabHolden,
                PlayerMovement.Singleton.Legs.GetComponent<Pickupable>().ScrapValue, PickupableType.Legs);
        }
        else
        {
            legsStored = null;
        }

        if (PlayerHealth.Singleton.Hull != null)
        {
            hullStored = new PickupableObjectInfoClass(PlayerHealth.Singleton.Hull.Durability,
                PlayerHealth.Singleton.Hull.GetComponent<Pickupable>().prefabReference.prefabHolden,
                PlayerHealth.Singleton.Hull.GetComponent<Pickupable>().ScrapValue, PickupableType.Hull);
        }
        else
        {
            hullStored = null;
        }
    }
    
    public void TurnItemIntoScrap(Pickupable thingToScrap, bool halfCost = false)
    {
        scrapCount += thingToScrap.ScrapValue / (halfCost ? 2 : 1);
        
        Destroy(thingToScrap.gameObject);
    }
    public void TurnItemIntoScrap(PickupableObjectInfoClass thingToScrap, bool halfCost = false)
    {
        scrapCount += thingToScrap.scrapValue / (halfCost ? 2 : 1);
        thingsStoredInBag.Remove(thingToScrap);
    }
}
