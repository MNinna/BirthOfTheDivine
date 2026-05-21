using PlayerScripts.Shooting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Bullet bullet;
    [SerializeField]
    private BulletPooling bulletPooling;
    [SerializeField]
    private float timer;
    private float _timerBuffer;

    private void Awake()
    {
        bulletPooling = GetComponent<BulletPooling>();
    }

    private void Start()
    {
        _timerBuffer = timer;
    }

    private void Update()
    {
        _timerBuffer -= Time.deltaTime;
        if (_timerBuffer > 0) return;

        var bullet = bulletPooling.GetPooledObject();
        bullet.Initialize(transform.position, Vector2.left);
        _timerBuffer = timer;
    }
}
