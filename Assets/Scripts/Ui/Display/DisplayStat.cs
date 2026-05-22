using UnityEngine;

namespace Ui.Display
{
    public abstract class DisplayStat : MonoBehaviour
    {
        protected int displayValue;
        
        protected void GetDisplayValue(int amount)
        {
            displayValue = amount;
        }
    }
}
