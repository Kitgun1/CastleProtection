using System;
using Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class MoveDirectionInput : IDisposable, IInputComponent
    {
        private readonly ScreenControl.MoveDirectionActions _moveDirection;
        
        public ScreenControl.MoveDirectionActions MoveDirection => _moveDirection;
        public InputAction ScreenPositionAction => MoveDirection.ScreenPosition;
        public InputAction ScreenDeltaAction => MoveDirection.ScreenDelta;
        
        
        public Vector2 ScreenPosition => ScreenPositionAction.ReadValue<Vector2>();
        public Vector2 ScreenDelta => ScreenDeltaAction.ReadValue<Vector2>();

        public MoveDirectionInput(ScreenControl.MoveDirectionActions moveDirection)
        {
            _moveDirection = moveDirection;
            _moveDirection.Enable();
        }


        public void Dispose()
        {
            // ReSharper disable once PossiblyImpureMethodCallOnReadonlyVariable
            _moveDirection.Disable();
        }
        
        ~MoveDirectionInput()
        {
            Dispose();
        }
    }
}