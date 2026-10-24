using UnityEngine;

/// <summary>
/// Zapętlone przewijanie tła w dół. Wrzuć na obiekt ze SpriteRenderer (tło).
/// Skrypt sam tworzy drugą kopię i układa ją nad pierwszą.
/// Wymaga, żeby grafika była bezszwowa w pionie.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1f;   // jednostki Unity na sekundę

    private Transform first;
    private Transform second;
    private float height;
    private float startY;

    private void Start()
    {
        var sr = GetComponent<SpriteRenderer>();
        height = sr.bounds.size.y;

        first = transform;
        startY = first.position.y;

        // druga kopia tuż nad pierwszą
        GameObject clone = Instantiate(gameObject, transform.parent);
        Destroy(clone.GetComponent<BackgroundScroller>());   // kopia nie przewija sama siebie
        second = clone.transform;
        second.position = new Vector3(first.position.x, startY + height, first.position.z);
    }

    private void Update()
    {
        float delta = scrollSpeed * Time.deltaTime;
        Move(first, delta);
        Move(second, delta);
    }

    private void Move(Transform t, float delta)
    {
        Vector3 p = t.position;
        p.y -= delta;

        // gdy kafelek zjedzie o całą wysokość w dół, przeskakuje na górę
        if (p.y <= startY - height)
            p.y += height * 2f;

        t.position = p;
    }
}