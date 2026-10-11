// Wspólny interfejs: implementuje go zarówno wróg, jak i gracz (łódź podwodna).
// Dzięki temu pociski gracza i wrogowie nie muszą znać konkretnych klas.
public interface IDamageable
{
    void TakeDamage(int amount);
}

// Implementuje go skrypt gracza - pickupy wywołują go po zebraniu.
public interface IPickupCollector
{
    void Collect(PickupData pickup);
}