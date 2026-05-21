using DefaultNamespace;
using UnityEngine;

namespace Bullets
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Vector2 _moveDirection;
        [SerializeField]
        protected float speed;
        [SerializeField] 
        private string objectToTargetTag;
        [SerializeField]
        protected int damage;
        [SerializeField] 
        private float TTL; // Time To Live - Racunalne mreze reference
        private float _timerBuffer;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        
        public void Initialize(Vector2 startingPosition, Vector2 direction)
        {
            transform.position = startingPosition;
            _moveDirection = direction;
            gameObject.SetActive(true);
            _timerBuffer = TTL;
        }
        
        private void Update()
        {
            _timerBuffer -= Time.deltaTime;
            if (_timerBuffer <= 0) gameObject.SetActive(false);
        }
        
        private void FixedUpdate()
        {
            Move();
        }
        
        private void Move()
        {
            _rb.MovePosition(_rb.position + _moveDirection * (speed * Time.fixedDeltaTime));
        }
        
        public virtual void Hit(HealthComponent healthComponent)
        {
            gameObject.SetActive(false);
            healthComponent?.TakeDamage(damage);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Failsafes
            if (objectToTargetTag == "") return;
            if (!other.CompareTag(objectToTargetTag)) return;
            other.TryGetComponent(typeof(HealthComponent), out var healthComponent);
            Hit((HealthComponent)healthComponent);
        }
    }
}
