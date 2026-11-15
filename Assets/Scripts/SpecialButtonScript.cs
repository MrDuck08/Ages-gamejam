using UnityEngine;
using UnityEngine.Events;

public class SpecialButtonScript : MonoBehaviour
{
    
    public void checkSpecialButton()
    {
        if (PlayerScrapAndBagManager.Singleton.ScrapCount >= PlayerScrapAndBagManager.Singleton.winScore)
        {
            DoThing.Invoke();
        }

    }
    public UnityEvent DoThing;
}
