using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(Pickupable))]
public abstract class WeaponBaseClass : MonoBehaviour
{
    [Header("Base settings")] 
    [SerializeField] private float durability;
    [SerializeField] protected List<Transform> shootPoints = new();
    [SerializeField] private float fireRate;
    [SerializeField] private float defaultDurability;
    [SerializeField] private Animator weaponAnimator;
    private bool startedAnimation = false;
    private float animationTime = 0;
    private float fireAnimationTime = 0;
    [Tooltip("This is just for the fist.")]
    [SerializeField] private bool doesNotLoseDurability;

    [Header("UI visuals")] 
    public Sprite weaponIcon;
    
    public float DefaultDurability => defaultDurability; //for UI
    public bool DoesNotLoseDurability => doesNotLoseDurability; //for UI
    
    public UnityEvent<WeaponBaseClass>  OnDurabilityRunOut = new();

    private Pickupable pickupable;
    [HideInInspector] public AudioManager audioManager;
    
    private float betweenShotsTimer;

    private bool durabilityHasAlreadyBeenSetFix;
    
    public float Durability
    {
        get => durability;
        set => durability = value;
    }

    private void Start()
    {
        tag = "Weapon";
        if (!durabilityHasAlreadyBeenSetFix)
        {
            durability = defaultDurability;   
        }
        pickupable = GetComponent<Pickupable>();
        audioManager = FindAnyObjectByType<AudioManager>();

        AnimationClip[] clips = weaponAnimator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if(clip.name == "FireAnimation")
            {
                fireAnimationTime = clip.length;
            }
        }
    }

    public virtual void Enter()
    {
        betweenShotsTimer = 0.05f;
    }

    public virtual void Update()
    {
        if (betweenShotsTimer > 0) betweenShotsTimer -= Time.deltaTime;


        if(weaponAnimator != null)
        {
            if (startedAnimation)
            {
                animationTime -= Time.deltaTime;

                if(animationTime <= 0)
                {
                    weaponAnimator.SetTrigger("ShotDone");
                    startedAnimation = false;
                }
            }
        }
    }

    public void InputTryShootWeapon()
    {
        if (betweenShotsTimer <= 0)
        {
            //WE CAN FIRE LAD!
            ChargeUp();
            betweenShotsTimer = fireRate;

            if(weaponAnimator != null)
            {
                weaponAnimator.SetTrigger("Shot");
                animationTime = fireAnimationTime;
                startedAnimation = true;
            }

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

    protected abstract void ChargeUp();

    public void SetDurabilityAtSetup(float toSetDurability)
    {
        durabilityHasAlreadyBeenSetFix = true;
        durability = toSetDurability;
    }
}