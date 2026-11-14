using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private float pickUpRadius;
    [SerializeField] private LayerMask layermaskOfPickupAbles;

    private Pickupable currentThingWeCanTryToPickUp;
    
    private void Update()
    {
        Collider2D[] findings = Physics2D.OverlapCircleAll(transform.position, pickUpRadius, layermaskOfPickupAbles);
        if (findings.Length != 0)
        {
            findings = findings.OrderBy(e => Vector3.Distance(e.gameObject.transform.position, transform.position)).ToArray();
            Debug.Log("The current thing we would like to pick up is: " + findings[0].gameObject.name);
            currentThingWeCanTryToPickUp = findings[0].GetComponent<Pickupable>();
        }
        else
        {
            currentThingWeCanTryToPickUp = null;
        }
    }

    private void TryPickUpLeft(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        if (currentThingWeCanTryToPickUp != null)
        {
            //try to do that. Pick it up in left arm
        }
    }
    private void TryPickUpRight(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        if (currentThingWeCanTryToPickUp != null)
        {
            //try to do that. Pick it up in right arm
        }
    }
    private void TryPickUpForBag(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        if (currentThingWeCanTryToPickUp != null)
        {
            //try to do that. Put it in the bag
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.aquamarine;
        Gizmos.DrawWireSphere(transform.position, pickUpRadius);
    }
}
