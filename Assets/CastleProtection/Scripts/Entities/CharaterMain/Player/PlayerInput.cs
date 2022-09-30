using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerGun _playerGun;

    private Rigidbody _rigidbody;
    private TouchController _touchController;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _touchController.Enable();
        _touchController.Touch.InputAction.performed += TouchPerformed;
    }

    private void OnDisable()
    {
        _touchController.Touch.InputAction.performed -= TouchPerformed;
        _touchController.Disable();
    }

    private void TouchPerformed(InputAction.CallbackContext performed)
    {
        _playerGun.TryShoot();
    }
}