using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public float DurationLife;

    private void Start()
    {
        StartCoroutine(BulletLifeTime(DurationLife));
    }

    private IEnumerator BulletLifeTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
