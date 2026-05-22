using System;
using Components.HealthComponent;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ui.Display
{
    public class DisplayHealth : DisplayStat
    {
        private Slider healthSlider;

        private void Awake()
        {
            healthSlider = GetComponentInChildren<Slider>();
        }

        private void Start()
        {
            GameEventManager.Instance.uiEvents.UpdateHealth += GetDisplayValue;
            GameEventManager.Instance.sceneEvents.SceneLoaded += UpdateSlider;
        }

        protected override void GetDisplayValue(int amount)
        {
            base.GetDisplayValue(amount);
            healthSlider.value = amount;
        }

        private void UpdateSlider()
        {
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();
            healthSlider.highValue = player.GetMaxHealth();
            healthSlider.value = player.GetCurrentHealth();
        }
    }
}