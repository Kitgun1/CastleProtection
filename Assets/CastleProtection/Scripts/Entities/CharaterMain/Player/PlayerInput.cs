using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerGun _playerGun;

    private TouchController _touchController;
    private Camera _camera;

    private void Awake()
    {
        _touchController = new TouchController();
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        print(_touchController);
        _touchController.Enable();
        _touchController.GameTouch.InputAction.performed += TouchPerformed;
    }

    private void OnDisable()
    {
        _touchController.GameTouch.InputAction.performed -= TouchPerformed;
        _touchController.Disable();
    }

    private void TouchPerformed(InputAction.CallbackContext performed)
    {
        Vector3 direction = GetDirection().normalized;
        Quaternion rotation = GetRotation(direction);
        _playerGun.TryShoot(direction, rotation);
        _playerGun.LookToDirection(direction);
    }

    private Vector3 GetDirection()
    {
        Vector2 screenPosition = _touchController.GameTouch.InputPosition.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, _camera.transform.position.y));
        return new Vector3(mouseWorldPosition.x, 0f, mouseWorldPosition.z) - transform.position;
    }
    private Quaternion GetRotation(Vector3 direction)
    {
        float angle = (Mathf.Atan2(direction.x, direction.z) - Mathf.PI / 2) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.up);
    }
}