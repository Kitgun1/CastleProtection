using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private EntityBulletData _entityBulletData;

    private Touch _touch;
    private EntityBullet _entityBullet;
    private IEnumerator _bulletShooted = null;

    private Vector3 _dir;
    private Camera _camera => Camera.main;

    private void Awake()
    {
        _touch = new Touch();
    }

    private void OnEnable()
    {
        _touch.Enable();
        _touch.FireControll.TouchInput.performed += ContactPerformed;
    }

    private void OnDisable()
    {
        _touch.FireControll.TouchInput.performed -= ContactPerformed;
        _touch.Disable();
    }

    private void Start()
    {
        _entityBullet = new EntityBullet(_entityBulletData);
    }

    private IEnumerator CooldownBulletShoot(float cooldown)
    {
        Vector2 screenPosition = _touch.FireControll.TouchPosition.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, _camera.transform.position.y));
        Vector2 mouseWorldPosition2 = new Vector2(mouseWorldPosition.x, mouseWorldPosition.z);
        Vector3 direction = new Vector3(mouseWorldPosition2.x, 0f, mouseWorldPosition2.y) - transform.position;

        transform.LookAt(mouseWorldPosition);
        var temp = (Mathf.Atan2(direction.x, direction.z) - Mathf.PI / 2) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(temp, Vector3.up);

        print(rotation.eulerAngles + " >> " + temp);
        _entityBullet.Shoot(transform, transform.position, new Vector2(direction.x, direction.z).normalized, rotation);
        yield return new WaitForSeconds(cooldown);
        _bulletShooted = null;
    }

    private void ContactPerformed(InputAction.CallbackContext action)
    {
        TryFire();
    }

    private void TryFire()
    {
        if (_bulletShooted == null)
        {
            _bulletShooted = CooldownBulletShoot(_entityBulletData.CooldownShoots);
            StartCoroutine(_bulletShooted);
        }
    }
}
