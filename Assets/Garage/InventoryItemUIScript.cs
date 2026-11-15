using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItemUIScript : MonoBehaviour, IPointerClickHandler
{
    public bool markedForScrapping;
    public PickupableObjectInfoClass refToTheClass;

    [SerializeField] private GameObject markForScrap;
    [SerializeField] private TextMeshProUGUI scrapValueText;
    [SerializeField] private Image icon;

    [SerializeField] private Slider durabilitySlider;
    
    public void Init(PickupableObjectInfoClass refTo)
    {
        refToTheClass = refTo;
        switch (refTo.type)
        {
            case PickupableType.Weapon:
                durabilitySlider.maxValue = refToTheClass.prefab.GetComponent<WeaponBaseClass>().DefaultDurability;
                break;
            case PickupableType.Legs:
                durabilitySlider.maxValue = refToTheClass.prefab.GetComponent<BaseLegs>().DefaultDurability;
                break;
            case PickupableType.Hull:
                durabilitySlider.maxValue = 100f;
                break;
        }
        DoVisuals();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //select it
            Debug.Log("We selected it");
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //mark it for scrapping
            markedForScrapping = !markedForScrapping;
        }

        DoVisuals();
    }

    private void DoVisuals()
    {
        markForScrap.SetActive(markedForScrapping);

        durabilitySlider.value = refToTheClass.durability;
        scrapValueText.text = "Scrap Value: \n\n" + refToTheClass.scrapValue;
        
        //this should now set the right icon
        switch (refToTheClass.type)
        {
            case PickupableType.Weapon:
                icon.sprite = refToTheClass.prefab.GetComponent<WeaponBaseClass>().weaponIcon;
                break;
            case PickupableType.Legs:
                icon.sprite = refToTheClass.prefab.GetComponent<BaseLegs>().Icon;
                break;
            case PickupableType.Hull:
                icon.sprite = refToTheClass.prefab.GetComponent<BaseHull>().Icon;
                break;
        }
    }
}
