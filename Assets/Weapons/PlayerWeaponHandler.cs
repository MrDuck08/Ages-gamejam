using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerWeaponHandler : MonoBehaviour
{
    public static PlayerWeaponHandler Singleton;
    
    [Tooltip("This one exists outside of the weapon stack as we do not want it to be swapable to unless you run out of durability for your current weapon.")]
    [SerializeField] private WeaponBaseClass leftFistWeapon;
    [SerializeField] private WeaponBaseClass rightFistWeapon;
    
    [HideInInspector] public UnityEvent OnWeaponChange = new();
    
    [Space]
    [Space]
    
    [SerializeField] private WeaponBaseClass leftHandCurrentWeapon; //temporary serialization because of yeah
    [SerializeField] private WeaponBaseClass rightHandCurrentWeapon; //temporary serialization because of yeah

    public WeaponBaseClass LeftHandCurrentWeapon => leftHandCurrentWeapon;
    public WeaponBaseClass RightHandCurrentWeapon => rightHandCurrentWeapon;
    
    public void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }

        Singleton = this;
    }
    
    private void Start()
    {
        leftHandCurrentWeapon.OnDurabilityRunOut.AddListener(OnWeaponRunOutOfDurability);
        rightHandCurrentWeapon.OnDurabilityRunOut.AddListener(OnWeaponRunOutOfDurability);
    }

    public void ShootWeaponLeftHand(InputAction.CallbackContext context)
    {
        if(context.performed) leftHandCurrentWeapon.InputTryShootWeapon();
    }
    public void ShootWeaponRightHand(InputAction.CallbackContext context)
    {
        if(context.performed) rightHandCurrentWeapon.InputTryShootWeapon();
    }

    private void SwapToWeapon(WeaponBaseClass weaponToSwapTo, bool whichHand, bool fixForDurabilityRanOut)
    {
        WeaponBaseClass currentWeapon = whichHand ? rightHandCurrentWeapon : leftHandCurrentWeapon;
        
        currentWeapon.OnDurabilityRunOut.RemoveListener(OnWeaponRunOutOfDurability);
        if (!currentWeapon.DoesNotLoseDurability && !fixForDurabilityRanOut)
        {
            currentWeapon.Drop();
        }
        
        if (!whichHand)
        {
            leftHandCurrentWeapon = weaponToSwapTo;
            currentWeapon = leftHandCurrentWeapon;
        }
        else
        {
            rightHandCurrentWeapon = weaponToSwapTo;
            currentWeapon = rightHandCurrentWeapon;
        }
        
        currentWeapon.OnDurabilityRunOut.AddListener(OnWeaponRunOutOfDurability);
        currentWeapon.Enter();
        OnWeaponChange?.Invoke();
    }

    public void PickUpWeapon(WeaponBaseClass weaponToSwapTo, bool whichHand)
    {
        weaponToSwapTo.gameObject.transform.SetParent(transform);
        weaponToSwapTo.gameObject.transform.localPosition = Vector3.zero;
        weaponToSwapTo.gameObject.transform.localRotation = Quaternion.identity;
        
        SwapToWeapon(weaponToSwapTo, whichHand, false);
    }

    private void OnWeaponRunOutOfDurability(WeaponBaseClass weaponThatDied)
    {
        SwapToWeapon(weaponThatDied != leftHandCurrentWeapon ? rightFistWeapon : leftFistWeapon, weaponThatDied != leftHandCurrentWeapon, true);
        Destroy(weaponThatDied.gameObject);
    }

    public void TakeDurabilityDamage(float damage)
    {
        leftHandCurrentWeapon.TakeDurabilityDamage(damage / 2f);
        rightHandCurrentWeapon.TakeDurabilityDamage(damage / 2f);
    }
}