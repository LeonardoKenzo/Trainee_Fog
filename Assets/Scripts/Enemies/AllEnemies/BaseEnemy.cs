using System.Collections;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IDamageDealer
{
    [SerializeField] protected GameObject _deathObject;

    [SerializeField] protected EnemiesStatsSO _statsSO;
    protected EnemiesRuntimeStats _stats;

    private void Awake()
    {
        _stats = new EnemiesRuntimeStats(_statsSO);
    }

    //IDamageDealer functions ------------------------------------------
    public virtual void TakeDamage(float damage )
    {
        _stats.CurrentHP -= damage;
        //KillEnemy and add Points
        if (_stats.CurrentHP <= 0f)
        {
            PointsManager.Instance.AddPoints(_stats.PointsValue);
            KillEnemy();
        }
    }
    public int GetDamage()
    {
        return _stats.BaseDamage;
    }

    public virtual void KillEnemy()
    {
        Instantiate(_deathObject, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
