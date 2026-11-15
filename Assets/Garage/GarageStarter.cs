using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GarageStarter : MonoBehaviour
{
    private float goToGarageTimer;

    [SerializeField] private Slider UITimer;

    private void Start()
    {
        goToGarageTimer = Random.Range(60f, 120f);
        UITimer.maxValue = goToGarageTimer;
    }

    private void Update()
    {
        goToGarageTimer -= Time.deltaTime;
        UITimer.value = goToGarageTimer;
        if (goToGarageTimer <= 0)
        {
            GoToGarage();
        }
    }

    public void GoToGarage()
    {
        GameObject[] allWeaponsLayingOnTheGround = GameObject.FindGameObjectsWithTag("Weapon").Where(e => !e.GetComponent<Pickupable>().HasBeenPickedUp).ToArray();
        for (int i = allWeaponsLayingOnTheGround.Length - 1; i >= 0; i--)
        {
            //scrap em, and sell em for half the value
            PlayerScrapAndBagManager.Singleton.TurnItemIntoScrap(allWeaponsLayingOnTheGround[i].GetComponent<Pickupable>(), true);
        }
        
        //also somehow store our current weapons
        PlayerScrapAndBagManager.Singleton.RefreshWeaponInfoObjects();
        
        //swap to garage scene
        SceneManager.LoadScene("Garage");
    }
}
