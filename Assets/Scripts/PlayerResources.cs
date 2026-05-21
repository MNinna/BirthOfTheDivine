using System;
using Events;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    [SerializeField] private int blood;
    [SerializeField] private int bones;

    private void OnEnable()
    {
        GameEventManager.Instance.resourceEvents.RewardBlood += AddBlood;
        GameEventManager.Instance.resourceEvents.RewardBones += AddBones;
    }

    private void OnDisable()
    {
        GameEventManager.Instance.resourceEvents.RewardBlood -= AddBlood;
        GameEventManager.Instance.resourceEvents.RewardBones -= AddBones;
    }

    private void AddBlood(int amount)
    {
        blood += amount;
    }
    
    private void AddBones(int amount)
    {
        bones += amount;
    }
}