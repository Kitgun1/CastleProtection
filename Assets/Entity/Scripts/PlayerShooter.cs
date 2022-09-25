using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private EntityBulletData _entityBulletData;

    private EntityBullet _entityBullet;
    private IEnumerator _bulletShooted = null;

    private void Start()
    {
        _entityBullet = new EntityBullet(_entityBulletData);
    }

    private void FixedUpdate()
    {
        if (_bulletShooted == null && Input.GetMouseButton(0))
        {
            _bulletShooted = CooldownBulletShoot(_entityBulletData.CooldownShoots);
            StartCoroutine(_bulletShooted);
        }
    }

    private IEnumerator CooldownBulletShoot(float cooldown)
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPosition - transform.position;
        Quaternion rotation = Quaternion.FromToRotation(transform.position, direction);
        direction = direction.normalized;

        _entityBullet.Shoot(transform, transform.position, direction, rotation);
        yield return new WaitForSeconds(cooldown);
        _bulletShooted = null;
    }
}
