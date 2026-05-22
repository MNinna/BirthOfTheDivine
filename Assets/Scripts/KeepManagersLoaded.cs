using System;
using UnityEngine;

public class KeepManagersLoaded : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
