using System;
using UnityEngine;

namespace Events
{
    public class ResourceEvents
    {
        public event Action<int> RewardBlood;
        public void OnRewardBlood(int amount)
        {
            RewardBlood?.Invoke(amount);
        }

        public event Action<int> RewardBones;
        public void OnRewardBones(int amount)
        {
            RewardBones?.Invoke(amount);
        }
    }
}
