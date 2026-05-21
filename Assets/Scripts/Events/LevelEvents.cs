using System;

namespace Events
{
    public class LevelEvents
    {
        public event Action LevelTimerFinished;
        public void OnLevelTimerFinished()
        {
            LevelTimerFinished?.Invoke();
        }
        
        
    }
}
