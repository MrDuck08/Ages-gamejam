using System;
using UnityEngine;

[Serializable]
public class PickupableObjectInfoClass
{
    public float durability;
    public GameObject prefab;
    public int scrapValue;
    public PickupableType type;
    
    public PickupableObjectInfoClass(float durability, GameObject prefab, int scrapValue, PickupableType type)
    {
        this.durability = durability;
        this.prefab = prefab;
        this.scrapValue = scrapValue;
        this.type = type;
    }
}