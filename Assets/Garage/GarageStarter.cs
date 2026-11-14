using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GarageStarter : MonoBehaviour
{
    public void GoToGarage()
    {
        GameObject[] allWeaponsLayingOnTheGround = GameObject.FindGameObjectsWithTag("Weapon").Where(e => !e.GetComponent<Pickupable>().HasBeenPickedUp).ToArray();
        for (int i = allWeaponsLayingOnTheGround.Length - 1; i >= 0; i--)
        {
            //scrap em, and sell em for half the value
            PlayerScrapAndBagManager.Singleton.TurnItemIntoScrap(allWeaponsLayingOnTheGround[i].GetComponent<Pickupable>(), true);
        }
        
        //swap to garage scene
        SceneManager.LoadScene("Garage");
    }
}
