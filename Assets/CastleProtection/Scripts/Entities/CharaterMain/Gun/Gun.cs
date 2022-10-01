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
}

[System.Serializable]
public struct GunData
{
    public GameObject BulletTemplate;
    [Range(0f, 120f)] public float CooldownShoot;
}