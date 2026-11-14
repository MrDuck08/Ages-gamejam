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

    [Header("UI visuals")] 
    public Sprite weaponIcon;
    
    public float DefaultDurability => defaultDurability; //for UI
    public bool DoesNotLoseDurability => doesNotLoseDurability; //for UI
    
    
    public UnityEvent<WeaponBaseClass>  OnDurabilityRunOut = new();

    private Pickupable pickupable;
    
    private float betweenShotsTimer;
    private float durability;
    public float Durability => durability;

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
            ShootWeapon();
            betweenShotsTimer = fireRate;

            TakeDurabilityDamage(1);
        }
    }

    public virtual void Drop()
    {
        pickupable.HasBeenPickedUp = false;
        transform.SetParent(null);
    }

    public void TakeDurabilityDamage(float damage)
    {
        if(!doesNotLoseDurability) durability -= damage;
        if (durability <= 0)
        {
            OnDurabilityRunOut?.Invoke(this);
        }
    }

    protected abstract void ShootWeapon();
}