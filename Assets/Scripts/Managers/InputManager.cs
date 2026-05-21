using Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    /// <summary>
    /// Input manager contains methods that the PlayerInput component uses to call event invokers in InputEvents.
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoBehaviour
    {
        public void MovePressed(InputAction.CallbackContext context)
        {
            GameEventManager.Instance.inputEvents.OnMovePressed(context);
        }

        public void AttackPressed(InputAction.CallbackContext context)
        {
            GameEventManager.Instance.inputEvents.OnAttackPressed(context);
        }

        public void MouseMoved(InputAction.CallbackContext context)
        {
            GameEventManager.Instance.inputEvents.OnMouseMoved(context);
        }

        public void QuitGame(InputAction.CallbackContext context)
        {
            GameEventManager.Instance.inputEvents.OnQuitGame(context);
        }

        public void PauseGame(InputAction.CallbackContext context)
        {
            GameEventManager.Instance.inputEvents.OnPauseGame(context);
        }
    }
}
