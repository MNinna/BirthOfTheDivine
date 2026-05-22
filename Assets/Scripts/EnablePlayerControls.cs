using Events;
using UnityEngine;

public class EnablePlayerControls : MonoBehaviour
{
    private void Start()
    {
        GameEventManager.Instance.sceneEvents.OnSceneLoaded();
    }
}
