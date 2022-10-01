using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private EntityBulletData _bulletData;

    private EntityBullet _entityBullet;

    private void Awake()
    {
        _entityBullet = new EntityBullet(_bulletData);
    }

    public void SetVelocity(Vector3 direction) => _entityBullet.AddForce(direction);

    public void SetRotation(Vector3 direction) => _entityBullet.SetRotation(gameObject, direction);

    public void SetLifeTime() => StartCoroutine(_entityBullet.BulletLife(gameObject));
}
