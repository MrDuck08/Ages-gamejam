using System;
using UnityEngine;
using UnityEngine.UI;

public class MechPartsUIBuilder : MonoBehaviour
{
    [SerializeField] private Image leftArmIcon;
    [SerializeField] private Slider leftArmDurability;
    
    [SerializeField] private Image rightArmIcon;
    [SerializeField] private Slider rightArmDurability;
    
    [SerializeField] private Image hullIcon;
    [SerializeField] private Slider hullDurability;
    
    [SerializeField] private Image legsIcon;
    [SerializeField] private Slider legsDurability;

    private void Update()
    {
        //yeah, really bad performance...
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        UpdateDurabilityValue();
        UpdateIconSprite();
    }
    
    private void UpdateDurabilityValue()
    {
        if (PlayerScrapAndBagManager.Singleton.LeftHandStoredWeapon != null)
        {
            leftArmDurability.maxValue = PlayerScrapAndBagManager.Singleton.LeftHandStoredWeapon.prefab.GetComponent<WeaponBaseClass>().DefaultDurability;
            leftArmDurability.value = PlayerScrapAndBagManager.Singleton.LeftHandStoredWeapon.durability;
        }
        else
        {
            leftArmDurability.value = 0;
            leftArmDurability.maxValue = 0;
        }

        if (PlayerScrapAndBagManager.Singleton.RightHandStoredWeapon != null)
        {
            rightArmDurability.maxValue = PlayerScrapAndBagManager.Singleton.RightHandStoredWeapon.prefab.GetComponent<WeaponBaseClass>().DefaultDurability;
            rightArmDurability.value = PlayerScrapAndBagManager.Singleton.RightHandStoredWeapon.durability;
        }        
        else
        {
            rightArmDurability.value = 0;
            rightArmDurability.maxValue = 0;
        }
        
        hullDurability.maxValue = PlayerScrapAndBagManager.Singleton.HullStored != null ? 100f : 0f;
        hullDurability.value = PlayerScrapAndBagManager.Singleton.HullStored != null ? PlayerScrapAndBagManager.Singleton.HullStored.durability : 0f;
        
        if (PlayerScrapAndBagManager.Singleton.LegsStored != null)
        {
            legsDurability.maxValue = PlayerScrapAndBagManager.Singleton.LegsStored.prefab.GetComponent<BaseLegs>().DefaultDurability;
            legsDurability.value = PlayerScrapAndBagManager.Singleton.LegsStored.durability;
        }
        else
        {
            legsDurability.value = 0;
            legsDurability.maxValue = 0;
        }
    }

    private void UpdateIconSprite()
    {
        leftArmIcon.sprite = PlayerScrapAndBagManager.Singleton.LeftHandStoredWeapon != null ? PlayerScrapAndBagManager.Singleton.LeftHandStoredWeapon.prefab.GetComponent<WeaponBaseClass>().weaponIcon : null;
        rightArmIcon.sprite = PlayerScrapAndBagManager.Singleton.RightHandStoredWeapon != null ? PlayerScrapAndBagManager.Singleton.RightHandStoredWeapon.prefab.GetComponent<WeaponBaseClass>().weaponIcon : null;
        
        hullIcon.sprite = PlayerScrapAndBagManager.Singleton.HullStored != null ? PlayerScrapAndBagManager.Singleton.HullStored.prefab.GetComponent<BaseHull>().Icon : null;
        legsIcon.sprite = PlayerScrapAndBagManager.Singleton.LegsStored != null ? PlayerScrapAndBagManager.Singleton.LegsStored.prefab.GetComponent<BaseLegs>().Icon : null;
    }
}
