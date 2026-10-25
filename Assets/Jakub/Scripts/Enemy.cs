using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyData data;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float hitFlashTime = 0.08f;

    /// <summary>Wywoływane po zabiciu - przyda się do punktów / liczenia wrogów na poziomie.</summary>
    public static event Action<Enemy> Died;

    public EnemyData Data => data;
    public int CurrentHealth { get; private set; }

    private Color baseColor = Color.white;

    private void Awake()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
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
        if (data == null) return;

        foreach (var entry in data.drops)
        {
            if (entry.pickupPrefab == null) continue;
            if (UnityEngine.Random.value > entry.chance) continue;

            // lekki rozrzut, żeby kilka pickupów nie wypadało z jednego punktu
            Vector3 offset = new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0f, 0f);
            Instantiate(entry.pickupPrefab, transform.position + offset, Quaternion.identity);
        }
    }

    // Wróg ma Collider2D z "Is Trigger" i Rigidbody2D (Kinematic).
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (data == null) return;
        if (other.TryGetComponent(out IPickupCollector _) && other.TryGetComponent(out IDamageable player))
        {
            player.TakeDamage(data.contactDamage);
            // opcjonalnie: wróg ginie przy zderzeniu (jak kamikaze) -> TakeDamage(CurrentHealth);
        }
    }
}