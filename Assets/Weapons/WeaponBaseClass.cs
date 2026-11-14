using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class WeaponBaseClass : MonoBehaviour
{
    [Header("Base settings")]
    [SerializeField] protected List<Transform> shootPoints = new();
    [SerializeField] private float fireRate;
    [SerializeField] private float defaultDurability;
    [Tooltip("This is just for the fist.")]
    [SerializeField] private bool doesNotLoseDurability;

    public float DefaultDurability => defaultDurability;
    public bool DoesNotLoseDurability => doesNotLoseDurability;
    
    
    public UnityEvent<WeaponBaseClass>  OnDurabilityRunOut = new();
    
    private float betweenShotsTimer;
    private float durability;

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
            
            ShootWeapon();
            betweenShotsTimer = fireRate;

            if (durability <= 0)
            {
                OnDurabilityRunOut?.Invoke(this);
            }
        }
    }
    public virtual void Exit(){}

    protected abstract void ShootWeapon();
}