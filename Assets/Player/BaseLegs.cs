using UnityEngine;

[RequireComponent(typeof(Pickupable))]
public class BaseLegs : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed;
    public float Speed => speed;
    [SerializeField] private float maxSpeed;
    public float MaxSpeed => maxSpeed;
    [Tooltip("Making this lower will make movement floatier")]
    [SerializeField] private float linearDampening;
    public float LinearDampening => linearDampening;
    
    [SerializeField] private float defaultDurability;
    public float DefaultDurability => defaultDurability;
    
    [Header("Visuals")] 
    [SerializeField] private Sprite icon;
}
