using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DropEntry
{
    [Tooltip("Prefab pickupa (z komponentem Pickup i przypisanym PickupData).")]
    public Pickup pickupPrefab;

    [Range(0f, 1f), Tooltip("Szansa na wypadnięcie (1 = zawsze).")]
    public float chance = 0.5f;
}

[CreateAssetMenu(fileName = "Enemy_", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Opis")]
    public string displayName;

    [Header("Statystyki")]
    [Min(1)] public int maxHealth = 1;
    [Min(0)] public int contactDamage = 1;   // obrażenia przy zderzeniu z łodzią

    [Header("Drop")]
    [Tooltip("Każdy wpis losowany niezależnie. Mocniejszy wróg = pickup z większą wartością.")]
    public List<DropEntry> drops = new List<DropEntry>();
}