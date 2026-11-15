using UnityEngine;
using UnityEngine.EventSystems;

public class MechPartUIInteractionHandler : MonoBehaviour, IPointerClickHandler
{
    public int whatAreYou;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //put it in the inventory
            PlayerScrapAndBagManager.Singleton.PutThingInBag(whatAreYou);
            GarageManager.Singleton.ReBuildInventory();
        }
    }
}
