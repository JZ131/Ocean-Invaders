using UnityEngine;

/// <summary>
/// Dokładasz do prefabu wroga, który ma strzelać. Wrogowie bez tego komponentu nie strzelają.
/// </summary>
public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private EnemyProjectile projectilePrefab;
    [SerializeField] private Transform firePoint;          // puste dziecko w miejscu wylotu; jeśli brak - pozycja wroga

    [Header("Parametry strzału")]
    [SerializeField, Min(1)] private int projectileDamage = 1;
    [SerializeField] private float projectileSpeed = 4f;
    [SerializeField] private float fireInterval = 2.5f;    // średni czas między strzałami (s)
    [SerializeField, Range(0f, 1f)] private float intervalJitter = 0.3f;  // losowe odchylenie, żeby ryby nie strzelały równo

    private float timer;

    private void OnEnable()
    {
        // losowy start, żeby cała fala nie wystrzeliła w tej samej klatce
        timer = Random.Range(0.2f, fireInterval);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0f) return;

        Shoot();
        float jitter = fireInterval * intervalJitter;
        timer = fireInterval + Random.Range(-jitter, jitter);
    }

    private void Shoot()
    {
        if (projectilePrefab == null) return;

        Vector3 pos = firePoint != null ? firePoint.position : transform.position;
        EnemyProjectile p = Instantiate(projectilePrefab, pos, Quaternion.identity);
        p.Init(projectileDamage, projectileSpeed);   // domyślnie w dół
    }
}