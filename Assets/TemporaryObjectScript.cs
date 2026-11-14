using System;
using UnityEngine;

public class TemporaryObjectScript : MonoBehaviour
{
    [SerializeField] private float HowLongDoesItLive;
    private float timerAlive;
    
    private void Update()
    {
        timerAlive += Time.deltaTime;
        if (timerAlive >= HowLongDoesItLive)
        {
            Destroy(gameObject);
        }
    }
}
