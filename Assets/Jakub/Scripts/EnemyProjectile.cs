using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float lifetime = 6f;   // zabezpieczenie, gdyby pocisk nigdzie nie trafił

    private int damage;
    private float speed;
    private Vector2 direction = Vector2.down;

    /// <summary>Wywoływane przez EnemyShooter zaraz po Instantiate.</summary>
    public void Init(int damage, float speed, Vector2? dir = null)
    {
        this.damage = damage;
        this.speed = speed;
        if (dir.HasValue) direction = dir.Value.normalized;
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Trafia tylko w gracza - ignoruje wrogów i pickupy.
        if (!other.CompareTag("Player")) return;

        if (other.TryGetComponent(out IDamageable target))
            target.TakeDamage(damage);

        Destroy(gameObject);
    }
}