using UnityEngine;

public class Gun
{
    private GunData _gunData;

    public Gun(GunData data)
    {
        _gunData = data;
    }

    public void Shoot(GameObject bullet, Vector3 startPosition, Vector3 direction, Quaternion rotation)
    {
        var bulletObj = Object.Instantiate(bullet, startPosition, rotation);
        Bullet bulletScript;
        if (bulletObj.TryGetComponent(out bulletScript))
        {
            Debug.Log(bulletScript == null);
            bulletScript.SetVelocity(direction);
            bulletScript.SetRotation(direction);
            bulletScript.SetLifeTime();
        }
    }

    public void SetGunRotation(Vector3 direction)
    {
        float angle = (Mathf.Atan2(direction.x, direction.z) - Mathf.PI / 2) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
        _gunData.Rigidbody.rotation = rotation;
    }
}

[System.Serializable]
public struct GunData
{
    public GameObject BulletTemplate;
    public Rigidbody Rigidbody;
    [Range(0f, 120f)] public float CooldownShoot;
}