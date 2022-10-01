using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private EntityBulletData _entityBulletData;

    private Camera _camera => Camera.main;

    private IEnumerator CooldownBulletShoot(float cooldown)
    {
        Vector2 screenPosition =Vector2.zero; //_touch.FireControll.TouchPosition.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, _camera.transform.position.y));
        Vector2 mouseWorldPosition2 = new Vector2(mouseWorldPosition.x, mouseWorldPosition.z);
        Vector3 direction = new Vector3(mouseWorldPosition2.x, 0f, mouseWorldPosition2.y) - transform.position;

        transform.LookAt(mouseWorldPosition);
        var temp = (Mathf.Atan2(direction.x, direction.z) - Mathf.PI / 2) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(temp, Vector3.up);

        //_entityBullet.Shoot(transform, transform.position, new Vector2(direction.x, direction.z).normalized, rotation);
        yield return new WaitForSeconds(cooldown);
    }
}
