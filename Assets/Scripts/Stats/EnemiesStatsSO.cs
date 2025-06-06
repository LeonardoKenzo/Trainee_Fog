using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesStatsSO", menuName = "Scriptable Objects/EnemiesStatsSO")]
public class EnemiesStatsSO : ScriptableObject
{
    [SerializeField] private float _currentHp;
    [SerializeField] private float _maxHp;
    [SerializeField] private float _baseDamage;
    [SerializeField] private float _moveSpeed;

    //Getters for each variable
    public float CurrentHp {
        get { return _currentHp; }
    }
    public float MaxHp { 
        get { return _maxHp; }
    }
    public float BaseDamage {
        get { return _baseDamage; }
    }
    public float MoveSpeed { 
        get { return _moveSpeed; }
    }
}
