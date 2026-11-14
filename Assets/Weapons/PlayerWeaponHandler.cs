using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponHandler : MonoBehaviour
{
    [Tooltip("This one exists outside of the weapon stack as we do not want it to be swapable to unless you run out of durability for your current weapon.")]
    [SerializeField] private WeaponBaseClass fistWeapon;
    
    [Space]
    [Space]
    
    [SerializeField] private WeaponBaseClass leftHandCurrentWeapon; //temporary serialization because of yeah
    [SerializeField] private WeaponBaseClass rightHandCurrentWeapon; //temporary serialization because of yeah

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
        WeaponBaseClass currentWeapon = whichHand ? leftHandCurrentWeapon : rightHandCurrentWeapon;
        
        currentWeapon.OnDurabilityRunOut.RemoveListener(OnWeaponRunOutOfDurability);
        if(currentWeapon != fistWeapon && !fixForDurabilityRanOut) currentWeapon.Drop();

        if (!whichHand) leftHandCurrentWeapon = weaponToSwapTo;
        else rightHandCurrentWeapon = weaponToSwapTo;
        
        currentWeapon.OnDurabilityRunOut.AddListener(OnWeaponRunOutOfDurability);
        currentWeapon.Enter();
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
        SwapToWeapon(fistWeapon, weaponThatDied != leftHandCurrentWeapon, true);
        Destroy(weaponThatDied.gameObject);
    }
}