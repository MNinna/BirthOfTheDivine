using System;
using Events;
using TMPro;

namespace Ui.Display
{
    public class DisplayBlood : DisplayStat
    {
        private TMP_Text bloodText;
        
        private void Start()
        {
            GameEventManager.Instance.uiEvents.UpdateBlood += GetDisplayValue;
        }
        
        
    }
}