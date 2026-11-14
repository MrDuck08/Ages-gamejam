using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Slider ui_healthSlider;

    private void Start()
    {
        ui_healthSlider.maxValue = PlayerHealth.Singleton.Health;
    }

    private void Update()
    {
        ui_healthSlider.value = PlayerHealth.Singleton.Health;
    }
}
