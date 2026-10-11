using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyData data;
    [SerializeField] private Pickup pickupPrefab;        // jeden wspólny prefab pickupa
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float hitFlashTime = 0.08f;

    public static event Action<Enemy> Died;

    public EnemyData Data => data;
    public int CurrentHealth { get; private set; }

    private Color baseColor = Color.white;

    private void Awake()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            if (data != null && data.sprite != null) spriteRenderer.sprite = data.sprite;
            baseColor = spriteRenderer.color;
        }
        CurrentHealth = data != null ? data.maxHealth : 1;
    }

    public void TakeDamage(int amount)
    {
        if (CurrentHealth <= 0) return;

        CurrentHealth -= amount;
        if (CurrentHealth <= 0) { Die(); return; }

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            CancelInvoke(nameof(ResetColor));
            Invoke(nameof(ResetColor), hitFlashTime);
        }
    }

    private void ResetColor()
    {
        if (spriteRenderer != null) spriteRenderer.color = baseColor;
    }

    private void Die()
    {
        RollDrops();
        Died?.Invoke(this);
        Destroy(gameObject);
    }

    private void RollDrops()
    {
        if (data == null || pickupPrefab == null) return;

        foreach (var entry in data.drops)
        {
            if (entry.pickup == null) continue;
            if (UnityEngine.Random.value > entry.chance) continue;

            // lekki rozrzut, żeby kilka pickupów nie nakładało się na siebie
            Vector3 offset = new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0f, 0f);
            Pickup p = Instantiate(pickupPrefab, transform.position + offset, Quaternion.identity);
            p.Init(entry.pickup);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (data == null) return;
        if (other.TryGetComponent(out IPickupCollector _) && other.TryGetComponent(out IDamageable player))
        {
            player.TakeDamage(data.contactDamage);
        }
    }
}