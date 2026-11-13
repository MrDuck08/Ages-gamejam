using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBaseClass : MonoBehaviour
{
    [Header("Base settings")]
    [SerializeField] protected List<Transform> shootPoints = new();
    [SerializeField] private float fireRate;
    [SerializeField] private float defaultDurability;
    
    private float betweenShotsTimer;
    protected float durability;

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
            durability--;
            ShootWeapon();
            betweenShotsTimer = fireRate;
        }
    }
    public virtual void Exit(){}

    protected abstract void ShootWeapon();
}