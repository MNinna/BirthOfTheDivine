using System;
using Events;
using TMPro;

namespace Ui.Display
{
    public class DisplayBlood : DisplayStat
    {
        private TMP_Text bloodText;

        private void Awake()
        {
            bloodText = GetComponentInChildren<TMP_Text>();
        }

        private void Start()
        {
            GameEventManager.Instance.uiEvents.UpdateBlood += GetDisplayValue;
        }

        protected override void GetDisplayValue(int amount)
        {
            base.GetDisplayValue(amount);
            if (bloodText) bloodText.text = amount.ToString();
        }
    }
}