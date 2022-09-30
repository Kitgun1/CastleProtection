using System;
using Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
#pragma warning disable CS0108, CS0114

namespace GameSystem
{
    public class Game : MonoBehaviour
    {
        [Header("Data options:")]
        [SerializeField] private Vector2Int boardSize;

        [Header("Item options:")]
        [SerializeField] private GameBoard board;
        [SerializeField] private Camera camera;
        [SerializeField] private GameTileContentFactory gameTileContentFactory;

        private Ray _touchRay => camera.ScreenPointToRay(_moveDirectionAction.ScreenPosition.ReadValue<Vector2>());
        
        private ScreenControl _control;
        private ScreenControl.MoveDirectionActions _moveDirectionAction;

        private void Awake()
        {
            _control = new ScreenControl();
            _control.Enable();
            _moveDirectionAction = _control.MoveDirection;

            if (camera == null) throw new ArgumentNullException(nameof(camera));
        }

        private void Start()
        {
            board.Initialize(boardSize);
        }
        
        private void HandleTouch()
        {
            GameTile tile = board.GetTile(_touchRay);
            if (tile != null)
            {
                tile.Content = gameTileContentFactory.Get(GameTileContentType.Destination);
            }
        }
        
        
        private void OnPress1(InputAction.CallbackContext obj)
        {
            HandleTouch();
        }
        
        private void OnEnable()
        {
            _moveDirectionAction.Enable();
            _moveDirectionAction.Button0.performed += OnPress1;
        }
        
        private void OnDisable()
        {
            _moveDirectionAction.Disable();
            _moveDirectionAction.Button0.performed -= OnPress1;
        }
    }
}