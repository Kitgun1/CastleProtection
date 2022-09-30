using System.Collections;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private EntityBulletData _bulletData;

    private EntityBullet _entityBullet;
    private IEnumerator _shoot = null;

    private void Start()
    {
        _entityBullet = new EntityBullet(_bulletData);
    }

    public void TryShoot()
    {
        if (_shoot == null)
        {
            _shoot = CooldownShoot(_bulletData.CooldownShoot);
            StartCoroutine(_shoot);
        }
    }

    private IEnumerator CooldownShoot(float cooldown)
    {
        Shoot(_bulletData.Teplate, Vector3.zero, _bulletData.Teplate.transform.rotation);

        yield return new WaitForSeconds(cooldown);
        _shoot = null;
    }

    private void Shoot(GameObject bullet, Vector3 direction, Quaternion rotation)
    {
        var bulletObj = Instantiate(bullet, direction, rotation);
    }
}
