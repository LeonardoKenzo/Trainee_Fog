using UnityEngine;

public interface IDamageDealer
{
    int GetDamage();

    void TakeDamage(float damage);
}
