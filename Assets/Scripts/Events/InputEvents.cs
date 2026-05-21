using System;
using UnityEngine.InputSystem;

namespace Events
{
    public class InputEvents
    {
        public event Action<InputAction.CallbackContext> MovePressed;
        public void OnMovePressed(InputAction.CallbackContext direction)
        {
            MovePressed?.Invoke(direction);
        }

        public event Action<InputAction.CallbackContext> AttackPressed;
        public void OnAttackPressed(InputAction.CallbackContext isPressed)
        {
            AttackPressed?.Invoke(isPressed);
        }

        public event Action<InputAction.CallbackContext> MouseMoved;
        public void OnMouseMoved(InputAction.CallbackContext position)
        {
            MouseMoved?.Invoke(position);
        }
        
        public event Action<InputAction.CallbackContext> QuitGame;
        public void OnQuitGame(InputAction.CallbackContext isPressed)
        {
            QuitGame?.Invoke(isPressed);
        }
        
        public event Action<InputAction.CallbackContext> PauseGame;
        public void OnPauseGame(InputAction.CallbackContext isPressed)
        {
            PauseGame?.Invoke(isPressed);
        }
    }
}
