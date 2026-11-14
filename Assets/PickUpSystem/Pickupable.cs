using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pickupable : MonoBehaviour
{
    public PrefabHolder prefabReference;
    
    [SerializeField] private PickupableType pickUpType;
    [Tooltip("Has built in randomization offset ranging from -5 to +5")]
    [SerializeField] private int scrapValue;
    public PickupableType Type => pickUpType;
    public int ScrapValue
    {
        get => scrapValue;
        set => scrapValue = value;
    }

    [Tooltip("Okay so if we want the ability to drop weapons then we have to swap this back to false")]
    public bool HasBeenPickedUp;

    private void Start()
    {
        scrapValue += Random.Range(-5, 5); //built in randomization of scrap value
    }
}