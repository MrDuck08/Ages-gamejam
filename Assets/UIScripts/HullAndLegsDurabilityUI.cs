using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HullAndLegsDurabilityUI : MonoBehaviour
{
    [SerializeField] private Image hullIcon;
    [SerializeField] private Slider hullDurability;
    
    [SerializeField] private Image legIcon;
    [SerializeField] private Slider legDurability;
    
    private void Start()
    {
        hullDurability.maxValue = 100;
        
        PlayerMovement.Singleton.OnLegsChange.AddListener(UpdateDurabilityMaxValue);
        UpdateDurabilityMaxValue();
    }

    private void UpdateDurabilityMaxValue()
    {
        //TODO, ugly ass fix
        if (PlayerMovement.Singleton.Legs != null)
        {
            legDurability.maxValue = PlayerMovement.Singleton.Legs.DefaultDurability;
            legIcon.sprite = PlayerMovement.Singleton.Legs.Icon;   
        }
    }

    private void Update()
    {
        if (PlayerHealth.Singleton.Hull != null)
        {
            hullIcon.sprite = PlayerHealth.Singleton.Hull.Icon;
            hullDurability.value = PlayerHealth.Singleton.Hull.Durability;
        }
        else
        {
            hullDurability.value = 0;
        }
        
        legDurability.value = PlayerMovement.Singleton.Legs.Durability;
    }
}
