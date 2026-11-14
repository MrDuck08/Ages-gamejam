using UnityEngine;

[RequireComponent(typeof(Pickupable))]
public class BaseHull : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private float durability;

    public float Durability => durability;
    
    [Header("Visuals")] 
    [SerializeField] private Sprite icon;

    public Sprite Icon => icon;
    
    [SerializeField] private SpriteRenderer spriteRender;
    
    private Pickupable pickupable;
    
    private void Start()
    {
        pickupable = GetComponent<Pickupable>();
    }

    public void BePickedUp(Transform newParent)
    {
        pickupable.HasBeenPickedUp = true;
        transform.SetParent(newParent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    
    public float CalculateHowMuchLeftOver(float damage)
    {
        if (durability <= damage)
        {
            Destroy(gameObject);
            return damage - durability;
        }
        durability -= damage;
        return 0f;
    }

    public void DropSelf()
    {
        spriteRender.enabled = true;
        pickupable.HasBeenPickedUp = false;
        transform.SetParent(null);
    }
}
