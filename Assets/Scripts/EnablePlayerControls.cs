using System;
using Events;
using Unity.VisualScripting;
using UnityEngine;

public class EnablePlayerControls : MonoBehaviour
{
    private void Start()
    {
        GameEventManager.Instance.sceneEvents.OnSceneLoaded();
    }
}
