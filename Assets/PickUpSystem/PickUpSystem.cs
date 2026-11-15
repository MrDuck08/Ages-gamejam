using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private PlayerWeaponHandler weaponHandler;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Transform whatToPickUpIndicatorObject;
    
    [SerializeField] private float pickUpRadius;
    [SerializeField] private LayerMask layermaskOfPickupAbles;

    private Pickupable currentThingWeCanTryToPickUp;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }

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
            audioManager.EquipPartSoundPlay();

            if (currentThingWeCanTryToPickUp.Type == PickupableType.Weapon)
            {
                currentThingWeCanTryToPickUp.HasBeenPickedUp = true;
                weaponHandler.PickUpWeapon(currentThingWeCanTryToPickUp.GetComponent<WeaponBaseClass>(), false);
            }
            else if(currentThingWeCanTryToPickUp.Type == PickupableType.Legs)
            {
                playerMovement.TryPickUpNewLegs(currentThingWeCanTryToPickUp.GetComponent<BaseLegs>());
            }
            else if (currentThingWeCanTryToPickUp.Type == PickupableType.Hull)
            {
                playerHealth.SwapHull(currentThingWeCanTryToPickUp.GetComponent<BaseHull>());
            }
        }
    }
    public void TryPickUpRight(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        if (currentThingWeCanTryToPickUp != null)
        {
            audioManager.EquipPartSoundPlay();

            if (currentThingWeCanTryToPickUp.Type == PickupableType.Weapon)
            {
                currentThingWeCanTryToPickUp.HasBeenPickedUp = true;
                weaponHandler.PickUpWeapon(currentThingWeCanTryToPickUp.GetComponent<WeaponBaseClass>(), true);
            }
            else if(currentThingWeCanTryToPickUp.Type == PickupableType.Legs)
            {
                playerMovement.TryPickUpNewLegs(currentThingWeCanTryToPickUp.GetComponent<BaseLegs>());
            }
            else if (currentThingWeCanTryToPickUp.Type == PickupableType.Hull)
            {
                playerHealth.SwapHull(currentThingWeCanTryToPickUp.GetComponent<BaseHull>());
            }
        }
    }
    public void TryPickUpForBag(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        if (currentThingWeCanTryToPickUp != null)
        {
            audioManager.InBagSound();
            //put it in the bag
            PlayerScrapAndBagManager.Singleton.PutThingInBag(currentThingWeCanTryToPickUp);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.aquamarine;
        Gizmos.DrawWireSphere(transform.position, pickUpRadius);
    }
}
