using System.Collections;
using UnityEngine;

public class EntityBullet
{
    private EntityBulletData _data;

    public EntityBullet(EntityBulletData data) => _data = data;

    public void AddForce(Vector3 direction, ForceMode forceMode = ForceMode.Impulse)
    {
        Debug.Log(direction);
        _data.Rigidbody.AddForce(direction * _data.Speed, forceMode);
    }

    public void SetRotation(GameObject bullet, Vector3 direction)
    {
        float angle = (Mathf.Atan2(direction.x, direction.z) - Mathf.PI / 2) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
        bullet.transform.rotation = rotation;
    }

    public IEnumerator BulletLife(GameObject bullet)
    {
        yield return new WaitForSeconds(_data.LifeTime);
        Object.Destroy(bullet);
    }
}

[System.Serializable]
public struct EntityBulletData
{
    public Rigidbody Rigidbody;

    [Header("Properties")]
    [Range(0f, Mathf.Infinity)] public float Speed;
    [Range(0f, Mathf.Infinity)] public float LifeTime;
    public float Damage;
}