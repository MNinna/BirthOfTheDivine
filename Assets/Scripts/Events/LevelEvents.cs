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
        
        public event Action<int> LevelTimerStarted;
        public void OnLevelTimerStarted(int seconds)
        {
            LevelTimerStarted?.Invoke(seconds);
        }
    }
}
