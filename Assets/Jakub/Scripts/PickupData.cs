using UnityEngine;

public enum PickupType
{
    Points,         // waluta do ulepszeń między poziomami
    Shield,         // chwilowa ochrona przed obrażeniami
    SpecialAttack,  // chwilowy specjalny atak
    // dopisujcie kolejne tutaj (np. Heal, RapidFire)
}

[CreateAssetMenu(fileName = "Pickup_", menuName = "Game/Pickup Data")]
public class PickupData : ScriptableObject
{
    public string displayName;
    public PickupType type;

    [Tooltip("Dla Points: ile punktów. Dla pozostałych może zostać 0.")]
    public int value = 10;

    [Tooltip("Dla Shield / SpecialAttack: ile sekund trwa efekt.")]
    public float duration = 5f;

    public Sprite sprite;
    public float fallSpeed = 2f;
}