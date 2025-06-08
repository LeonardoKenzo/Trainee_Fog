using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    private static PlayerStatsManager instance;

    [Header("Health")]
    [SerializeField] private int _hpMax = 10;
    [SerializeField] private float _currentHp;

    [Header("Damage")]
    [SerializeField] private float _damage = 2f;
    public float Damage => _damage;

    [Header("Death")]
    [SerializeField] private GameObject _deathPanel;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        _currentHp = _hpMax;
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
        MusicManager.PauseMusic();
        _deathPanel.SetActive(true);
    }
}
