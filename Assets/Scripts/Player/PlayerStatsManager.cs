using System;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int _hpMax = 10;
    [SerializeField] private float _hp;

    [Header("Magic")]
    [SerializeField] private int _mpMax = 20;
    [SerializeField] private float _mp;

    private void Start()
    {
        _hp = _hpMax;
        _mp = _mpMax;
    }

    private void Update()
    {
        if (_hp <= 0)
        {
            Die();
        }
    }

    //hp functions
    public float GetHP() {  return _hp; }
    public void SetHP(float _hp) {
        if (_hp > _hpMax)
            _hp = _hpMax;
        else if (_hp < 0)
            _hp = 0;
        this._hp = _hp;
    }
    public void TakeDamage(int _damage) {_hp -= _damage; }
    public void Heal(float _healPoints) { _hp += _healPoints; }
    public void Die()
    {
        //die animation;
        Time.timeScale = 0;
        Debug.Log("Você morreu!");//temporary
    }

    //mp functions
    public float GetMP() { return _mp; }
    public void SetMP(float _mp) {
        if(_mp > _mpMax)
            _mp = _mpMax;
        else if(_mp < 0) 
            _mp = 0;
        this._mp = _mp; 
    }
    public void CastMagic(int _cost) { _mp -= _cost; }
    public void RestoreMagic(int _restaureMp) { _mp += _restaureMp; }
}
