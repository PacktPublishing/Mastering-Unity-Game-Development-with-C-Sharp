using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;  // Add this for EventSystem

namespace FusionFuryGame
{
    public class PlayerInput : MonoBehaviour , GamePlay.IGamePlayMapActions
    {
        private GamePlay gameplayControls;
        public static UnityAction onJump = delegate { };
        public static UnityAction onDash = delegate { };
        public static UnityAction<Vector2> onMovement = delegate { };
        public static UnityAction onShoot = delegate { };
        public static UnityAction onReload = delegate { };
        public static UnityAction onAbility = delegate { };
        private void OnEnable()
        {
            if (gameplayControls == null)
            {
                gameplayControls = new GamePlay();
                gameplayControls.GamePlayMap.SetCallbacks(this);
            }
            gameplayControls.GamePlayMap.Enable();       
        }


        private void OnDisable()
        {
            gameplayControls.GamePlayMap.Disable();     
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
                onDash.Invoke();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
                onJump.Invoke();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            onMovement.Invoke(context.ReadValue<Vector2>());
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started && !IsPointerOverUI())
                onShoot.Invoke();
        }

        public void OnReload(InputAction.CallbackContext context)
        {
            if (context.performed)
                onReload.Invoke();
        }

        public void OnAbility(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                //now i'm using UI for that
                //onAbility.Invoke();
            }
        }

        // This method checks if the pointer is over any UI element
        private bool IsPointerOverUI()
        {
            // Returns true if the pointer is over a UI element
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}