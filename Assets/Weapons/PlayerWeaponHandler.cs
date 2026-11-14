using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponHandler : MonoBehaviour
{
    [Tooltip("This one exists outside of the weapon stack as we do not want it to be swapable to unless you run out of durability for your current weapon.")]
    [SerializeField] private WeaponBaseClass fistWeapon;
    
    [SerializeField] private WeaponBaseClass leftHandCurrentWeapon; //temporary serialization because of yeah
    [SerializeField] private WeaponBaseClass rightHandCurrentWeapon; //temporary serialization because of yeah
    public void ShootWeaponLeftHand(InputAction.CallbackContext context)
    {
        if(context.performed) leftHandCurrentWeapon.InputTryShootWeapon();
    }
    public void ShootWeaponRightHand(InputAction.CallbackContext context)
    {
        if(context.performed) rightHandCurrentWeapon.InputTryShootWeapon();
    }

    public void SwapToWeapon(WeaponBaseClass weaponToSwapTo, bool whichHand)
    {
        WeaponBaseClass currentWeapon = whichHand ? leftHandCurrentWeapon : rightHandCurrentWeapon;
        
        currentWeapon.OnDurabilityRunOut.RemoveListener(OnWeaponRunOutOfDurability);
        currentWeapon.Exit();

        if (!whichHand) leftHandCurrentWeapon = weaponToSwapTo;
        else rightHandCurrentWeapon = weaponToSwapTo;
        
        currentWeapon.OnDurabilityRunOut.AddListener(OnWeaponRunOutOfDurability);
        currentWeapon.Enter();
    }

    private void OnWeaponRunOutOfDurability(WeaponBaseClass weaponThatDied)
    {
        if (weaponThatDied == leftHandCurrentWeapon)
        {
            SwapToWeapon(fistWeapon, weaponThatDied != leftHandCurrentWeapon);
        }
        
        //we ran out of durability, so let's kill it!
        Destroy(weaponThatDied.gameObject);
    }
}