using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDurabilityUI : MonoBehaviour
{
    [SerializeField] private Image leftHandWeaponIcon;
    [SerializeField] private Slider leftHandWeaponDurability;
    
    [SerializeField] private Image rightHandWeaponIcon;
    [SerializeField] private Slider rightHandWeaponDurability;

    private void Start()
    {
        PlayerWeaponHandler.Singleton.OnWeaponChange.AddListener(UpdateDurabilityMaxValue);
        UpdateDurabilityMaxValue();
    }

    private void UpdateDurabilityMaxValue()
    {
        leftHandWeaponDurability.maxValue = PlayerWeaponHandler.Singleton.LeftHandCurrentWeapon.DefaultDurability;
        rightHandWeaponDurability.maxValue = PlayerWeaponHandler.Singleton.RightHandCurrentWeapon.DefaultDurability;

        leftHandWeaponIcon.sprite = PlayerWeaponHandler.Singleton.LeftHandCurrentWeapon.weaponIcon;
        rightHandWeaponIcon.sprite = PlayerWeaponHandler.Singleton.RightHandCurrentWeapon.weaponIcon;
    }

    private void Update()
    {
        leftHandWeaponDurability.value = PlayerWeaponHandler.Singleton.LeftHandCurrentWeapon.Durability;
        rightHandWeaponDurability.value = PlayerWeaponHandler.Singleton.RightHandCurrentWeapon.Durability;
    }
}
