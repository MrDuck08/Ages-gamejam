using System;
using UnityEngine;

[Serializable]
public class PickupableObjectInfoClass
{
    public float durability;
    public GameObject prefab;
    public int scrapValue;
    
    public PickupableObjectInfoClass(float durability, GameObject prefab, int scrapValue)
    {
        this.durability = durability;
        this.prefab = prefab;
        this.scrapValue = scrapValue;
    }
}