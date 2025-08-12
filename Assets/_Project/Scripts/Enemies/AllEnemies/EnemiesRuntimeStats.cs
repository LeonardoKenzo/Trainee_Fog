using UnityEditor;
using UnityEngine;

[System.Serializable]
public class EnemiesRuntimeStats
{
    [SerializeField] private float _maxHp;
    [SerializeField] private float _currentHp;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _baseDamage;
    [SerializeField] private int _pointsValue;

    public float CurrentHP
    {
        get { return _currentHp; }
        set { _currentHp = Mathf.Clamp(value, 0f, _maxHp); }
    }
    public int BaseDamage => _baseDamage;
    public float MoveSpeed => _moveSpeed;
    public int PointsValue => _pointsValue;

    public EnemiesRuntimeStats(EnemiesStatsSO _enemiesStatsSo)
    {
        _currentHp = _enemiesStatsSo.CurrentHp;
        _maxHp = _enemiesStatsSo.MaxHp;
        _baseDamage = _enemiesStatsSo.BaseDamage;
        _moveSpeed = _enemiesStatsSo.MoveSpeed;
        _pointsValue = _enemiesStatsSo.PointsValue;
    }

}
