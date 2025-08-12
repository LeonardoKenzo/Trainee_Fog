using System.Collections;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IDamageDealer
{
    [SerializeField] protected GameObject _deathObject;

    [SerializeField] protected EnemiesStatsSO _statsSO;
    [SerializeField] protected EnemiesRuntimeStats _stats;

    protected virtual void Awake()
    {
        _stats = new EnemiesRuntimeStats(_statsSO);
    }

    public virtual IEnumerator BlinkDamage()
    {
        float _elapsed = 0f;
        float _blinkInterval = 0.1f;
        bool _isTransparent = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        while (_elapsed < 0.5f)
        {
            Color transparence = spriteRenderer.color;

            if (_isTransparent)
            {
                //Turns Invisible
                transparence.a = 0f;
                spriteRenderer.color = transparence;
                _isTransparent = false;
            }
            else if (!_isTransparent)
            {
                //Turns Visible
                transparence.a = 1f;
                spriteRenderer.color = transparence;
                _isTransparent = true;
            }

            yield return new WaitForSeconds(_blinkInterval);

            _elapsed += _blinkInterval;
        }

        //Guarantee the visibility of sprite 
        Color finalColor = spriteRenderer.color;
        finalColor.a = 1f;
        spriteRenderer.color = finalColor;
        _isTransparent = false;
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
