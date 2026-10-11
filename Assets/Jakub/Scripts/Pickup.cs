using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Pickup : MonoBehaviour
{
    [SerializeField] private PickupData data;            // opcjonalnie: do testów bezpośrednio na prefabie
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float destroyBelowY = -8f;  // dopasujcie do wysokości planszy

    public PickupData Data => data;

    private void Awake()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Apply();
    }

    /// <summary>Wywoływane przez Enemy zaraz po Instantiate.</summary>
    public void Init(PickupData newData)
    {
        data = newData;
        Apply();
    }

    private void Apply()
    {
        if (data != null && spriteRenderer != null && data.sprite != null)
            spriteRenderer.sprite = data.sprite;
    }

    private void Update()
    {
        float speed = data != null ? data.fallSpeed : 2f;
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);

        if (transform.position.y < destroyBelowY) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (data == null) return;
        if (other.TryGetComponent(out IPickupCollector collector))
        {
            collector.Collect(data);
            Destroy(gameObject);
        }
    }
}