using System;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int _hpMax = 10;
    [SerializeField] private float _currentHp;

    [Header("Magic")]
    [SerializeField] private int _mpMax = 20;
    [SerializeField] private float _currentMp;

    [Header("Damage")]
    [SerializeField] private float _damage = 2f;
    public float Damage => _damage;

    private void Start()
    {
        _currentHp = _hpMax;
        _currentMp = _mpMax;
    }

    //hp functions ----------------------------------------
    public float Hp
    {
        get { return _currentHp; }
        set { 
            _currentHp = Mathf.Clamp(value, 0, _hpMax);

            if (_currentHp <= 0)
                Die();
        }
    }
    public void TakeDamage(int _damage) {Hp -= _damage; }
    public void Heal(float _healPoints) { Hp += _healPoints; }
    public void Die()
    {
        //die animation;
        Time.timeScale = 0;
        Debug.Log("Você morreu!");//temporary
    }

    //mp functions -----------------------------------------
    public float Mp { 
        get { return _currentMp; }
        set { _currentMp = Mathf.Clamp(value, 0, _mpMax); }
    }
    public void CastMagic(int _cost) { Mp -= _cost; }
    public void RestoreMagic(int _restaureMp) { Mp += _restaureMp; }
}
