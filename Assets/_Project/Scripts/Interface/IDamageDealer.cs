using System.Collections;
using UnityEngine;

public interface IDamageDealer
{
    int GetDamage();

    void TakeDamage(float damage);

    void KillEnemy();
}
