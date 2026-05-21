using System;
using UnityEngine;

public class SceneEvents
{
    public event Action<string> ChangeScene;
    public void OnChangeScene(string sceneName)
    {
        ChangeScene?.Invoke(sceneName);
    }
}
