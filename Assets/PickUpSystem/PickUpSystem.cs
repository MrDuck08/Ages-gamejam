using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private PlayerWeaponHandler weaponHandler;
    [SerializeField] private Transform whatToPickUpIndicatorObject;
    
    [SerializeField] private float pickUpRadius;
    [SerializeField] private LayerMask layermaskOfPickupAbles;

    private Pickupable currentThingWeCanTryToPickUp;
    
    private void Update()
    {
        Collider2D[] findings = Physics2D.OverlapCircleAll(transform.position, pickUpRadius, layermaskOfPickupAbles);
        if (findings.Length != 0)
        {
            findings = findings.Where(e => !e.GetComponent<Pickupable>().HasBeenPickedUp).OrderBy(e => Vector3.Distance(e.gameObject.transform.position, transform.position)).ToArray();
            if (findings.Length != 0)
            {
                whatToPickUpIndicatorObject.gameObject.SetActive(true);
                whatToPickUpIndicatorObject.position = findings[0].gameObject.transform.position;
                currentThingWeCanTryToPickUp = findings[0].GetComponent<Pickupable>();
                return;
            }
        }
        
        whatToPickUpIndicatorObject.gameObject.SetActive(false);
        currentThingWeCanTryToPickUp = null;
    }

    public void TryPickUpLeft(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        if (currentThingWeCanTryToPickUp != null)
        {
            if (currentThingWeCanTryToPickUp.Type == PickupableType.Weapon)
            {
                currentThingWeCanTryToPickUp.HasBeenPickedUp = true;
                weaponHandler.PickUpWeapon(currentThingWeCanTryToPickUp.GetComponent<WeaponBaseClass>(), false);
            }
        }
    }
    public void TryPickUpRight(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        if (currentThingWeCanTryToPickUp != null)
        {
            if (currentThingWeCanTryToPickUp.Type == PickupableType.Weapon)
            {
                currentThingWeCanTryToPickUp.HasBeenPickedUp = true;
                weaponHandler.PickUpWeapon(currentThingWeCanTryToPickUp.GetComponent<WeaponBaseClass>(), true);
            }
        }
    }
    public void TryPickUpForBag(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        if (currentThingWeCanTryToPickUp != null)
        {
            //try to do that. Put it in the bag
            Debug.Log("We tried to put it in the bag.");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.aquamarine;
        Gizmos.DrawWireSphere(transform.position, pickUpRadius);
    }
}
