using System;
using Events;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private void Start()
    {
        GameEventManager.Instance.sceneEvents.ChangeScene += ChangeScene;
    }

    private void ChangeScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
