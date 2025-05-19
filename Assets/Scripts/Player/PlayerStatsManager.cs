using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int _hpMax = 5;
    [SerializeField] private int _hp;

    [Header("Magic")]
    [SerializeField] private int _mpMax = 20;
    [SerializeField] private int _mp;


    private void Start()
    {
        _hp = _hpMax;
        _mp = _mpMax;
    }

    //get set hp
    public int GetHP() {  return _hp; }
    public void SetHP(int _hp) {
        if (_hp > _hpMax)
            _hp = _hpMax;
        else if (_hp < 0)
            _hp = 0;
        this._hp = _hp;
    }

    //get set mp
    public int GetMP() { return _mp; }
    public void SetMP(int _mp) {
        if(_mp > _mpMax)
            _mp = _mpMax;
        else if(_mp < 0) 
            _mp = 0;
        this._mp = _mp; 
    }
}
