using UnityEngine;

public class Pickupable : MonoBehaviour
{
    [SerializeField] private PickupableType pickUpType;
    public PickupableType Type => pickUpType;

    [Tooltip("Okay so if we want the ability to drop weapons then we have to swap this back to false")]
    public bool HasBeenPickedUp;
}