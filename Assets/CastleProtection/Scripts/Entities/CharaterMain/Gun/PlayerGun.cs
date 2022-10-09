using System.Collections;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private GunData _gunData;

    private Gun _gun;
    private IEnumerator _shoot = null;

    private void Start()
    {
        _gun = new Gun(_gunData);
    }

    public void TryShoot(Vector3 direction, Quaternion rotation)
    {
        if (_shoot == null)
        {
            _shoot = CooldownShoot(_gunData.CooldownShoot, direction, rotation);
            StartCoroutine(_shoot);
        }
    }

    public void LookToDirection(Vector3 direction)
    {
        _gun.SetGunRotation(direction);
    }

    private IEnumerator CooldownShoot(float cooldown, Vector3 direction, Quaternion rotation)
    {
        _gun.Shoot(_gunData.BulletTemplate, transform.position, direction, rotation);

        yield return new WaitForSeconds(cooldown);
        _shoot = null;
    }
}