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
    
    [SerializeField] private float durability;
    [SerializeField] private bool IsInfiniteDurability;
    
    [Header("Visuals")] 
    [SerializeField] private Sprite icon;

    [SerializeField] private SpriteRenderer spriteRender;
    
    private Pickupable pickupable;
    
    private void Start()
    {
        pickupable = GetComponent<Pickupable>();
    }

    public void BePickedUp(Transform newParent)
    {
        spriteRender.enabled = false;
        pickupable.HasBeenPickedUp = true;
        transform.SetParent(newParent);
        transform.localPosition = Vector3.zero;
    }
    
    public bool TakeDurabilityDamage(float damage)
    {
        if (IsInfiniteDurability) return false;
        durability -= damage;
        if (durability <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    public void DropSelf()
    {
        spriteRender.enabled = true;
        pickupable.HasBeenPickedUp = false;
        transform.SetParent(null);
    }
}
