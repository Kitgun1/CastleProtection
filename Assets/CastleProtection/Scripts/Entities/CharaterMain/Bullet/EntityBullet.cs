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
        GameObject bulletObj = Object.Instantiate(_data.Teplate, startPos, rotation);
        Rigidbody bulletRigidbody = bulletObj.GetComponent<Rigidbody>();
        AddBulletVelocity(bulletRigidbody, direction);
    }

    public void AddForce(Vector3 direction, ForceMode forceMode = ForceMode.Impulse)
    {
        _data.Rigidbody.AddForce(direction, forceMode);
    }

    public void SetRotation(Vector3 direction)
    {
        var temp = (Mathf.Atan2(direction.x, direction.z) - Mathf.PI / 2) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(temp, Vector3.up);

        //_entityBullet.Shoot(transform, transform.position, new Vector2(direction.x, direction.z).normalized, rotation);
    }

    private void AddBulletVelocity(Rigidbody bullet, Vector2 direction2)
    {
        Vector3 direction3 = new Vector3(direction2.x, 0f, direction2.y);
        bullet.AddForce(direction3 * _data.Speed, ForceMode.Impulse);
    }

    //private void BulletLifeTime(GameObject bulletObj, float duration)
    //{
    //    bulletObj.GetComponent<Bullet>().DurationLife = duration;
    //}
}

public struct EntityBulletData
{
    [Header("Links")]
    public GameObject Teplate;
    public Rigidbody Rigidbody;

    [Header("Properties")]
    [Range(0f, Mathf.Infinity)] public float Speed;
    public float Damage;
    public float CooldownShoot;
}