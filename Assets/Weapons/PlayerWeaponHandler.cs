using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] private WeaponBaseClass currentWeapon; //temporary serialization because of yeah
    public void ShootWeapon(InputAction.CallbackContext context)
    {
        if(context.performed) currentWeapon.InputTryShootWeapon();
    }

    //also need a way to tell this weapon handler that we lost the weapon because of decrease of durability.
}