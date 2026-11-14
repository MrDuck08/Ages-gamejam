using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Pickupable))]
public abstract class WeaponBaseClass : MonoBehaviour
{
    [Header("Base settings")]
    [SerializeField] protected List<Transform> shootPoints = new();
    [SerializeField] private float fireRate;
    [SerializeField] private float defaultDurability;
    [Tooltip("This is just for the fist.")]
    [SerializeField] private bool doesNotLoseDurability;

    public float DefaultDurability => defaultDurability; //for UI
    public bool DoesNotLoseDurability => doesNotLoseDurability; //for UI
    
    
    public UnityEvent<WeaponBaseClass>  OnDurabilityRunOut = new();

    private Pickupable pickupable;
    
    private float betweenShotsTimer;
    private float durability;

    private void Start()
    {
        durability = defaultDurability;
        pickupable = GetComponent<Pickupable>();
    }

    public virtual void Enter()
    {
        betweenShotsTimer = fireRate;
    }

    public void Update()
    {
        if (betweenShotsTimer < 0) betweenShotsTimer -= Time.deltaTime;
    }

    public void InputTryShootWeapon()
    {
        if (betweenShotsTimer >= 0)
        {
            //WE CAN FIRE LAD!
            if(!doesNotLoseDurability) durability--;
            Debug.Log(durability); 
            
            ShootWeapon();
            betweenShotsTimer = fireRate;

            if (durability <= 0)
            {
                Debug.Log("Bitch!");
                OnDurabilityRunOut?.Invoke(this);
            }
        }
    }

    public virtual void Drop()
    {
        pickupable.HasBeenPickedUp = false;
        transform.SetParent(null);
    }

    protected abstract void ShootWeapon();
}