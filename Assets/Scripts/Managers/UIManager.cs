using System;
using Events;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Canvas pauseCanvas;

    private void Start()
    {
        GameEventManager.Instance.inputEvents.PauseGame += PauseGame;
    }

    private void PauseGame(InputAction.CallbackContext context)
    {
        pauseCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
