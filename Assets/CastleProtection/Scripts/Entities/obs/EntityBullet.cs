using UnityEngine;

public class EntityBullet
{
    private EntityBulletData _data;

    public EntityBullet(EntityBulletData data)
    {
        _data = data;
    }

    public void Shoot(Transform parent, Vector2 startPos, Vector2 direction, Quaternion rotation)
    {
        GameObject bulletObj = Object.Instantiate(_data.BulletObj, startPos, rotation, parent);
        Rigidbody bulletRigidbody = bulletObj.GetComponent<Rigidbody>();
        AddBulletVelocity(bulletRigidbody, direction);
        BulletLifeTime(bulletObj, _data.BulletLifeTime);
    }

    private void AddBulletVelocity(Rigidbody bullet, Vector2 direction)
    {
        bullet.AddForce(direction * _data.BulletSpeed, ForceMode.Impulse);
    }

    private void BulletLifeTime(GameObject bulletObj, float duration)
    {
        bulletObj.GetComponent<Bullet>().DurationLife = duration;
    }
}

[System.Serializable]
public struct EntityBulletData
{
    public GameObject BulletObj;
    public float BulletSpeed;
    public float BulletLifeTime;
    public float CooldownShoots;
}