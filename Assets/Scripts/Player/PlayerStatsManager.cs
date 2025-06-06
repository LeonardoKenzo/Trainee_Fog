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

    private void Start()
    {
        _currentHp = _hpMax;
        _currentMp = _mpMax;
    }

    private void Update()
    {
        if (_currentHp <= 0)
        {
            Die();
        }
    }

    //hp functions
    public float Hp
    {
        get { return _currentHp; }
    }
    public void TakeDamage(int _damage) {_currentHp -= _damage; }
    public void Heal(float _healPoints) { _currentHp += _healPoints; }
    public void Die()
    {
        //die animation;
        Time.timeScale = 0;
        Debug.Log("Você morreu!");//temporary
    }

    //mp functions
    public float Mp { 
        get { return _currentMp; }
    }
    public void CastMagic(int _cost) { _currentMp -= _cost; }
    public void RestoreMagic(int _restaureMp) { _currentMp += _restaureMp; }
}
