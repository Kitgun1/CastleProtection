using System;
using Entity.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity.Character
{
    public class CharacterMovement : MonoBehaviour
    {
        private CharacterControl _characterControl;
        private InputAction _move;
        private InputAction _jump;
        private InputAction _fire;
        private InputAction _fire2;

        public PhysicsMovement _physicsMovement;
        

        public void Awake()
        {
            _characterControl = new CharacterControl();
            _move = _characterControl.Character.MoveDirection;
            _jump = _characterControl.Character.Jump;
        }

        private void FixedUpdate()
        {
            OnMove();
        }

        public void OnEnable()
        {
            _move.Enable();
            _jump.Enable();

            //_move.performed += OnMove;
            _jump.performed += OnJump;
        }

        public void OnDisable()
        {
            _move.Disable();
            _jump.Disable();
        }

        private void OnMove()
        {
            _physicsMovement.Move(_move.ReadValue<Vector2>());
        }

        private void OnJump(InputAction.CallbackContext callbackContext)
        {
            _physicsMovement.Jump();
        }
    }
}