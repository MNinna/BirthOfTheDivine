using System;
using Events;
using UnityEngine;

public class GoToNextLevel : MonoBehaviour
{
    private Collider2D nextLevelTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        GameEventManager.Instance.levelEvents.OnLevelTimerFinished();
    }
}
