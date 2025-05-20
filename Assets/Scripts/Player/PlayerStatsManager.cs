using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStatsManager : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int _hpMax = 5;
    [SerializeField] private int _hp;

    [Header("Magic")]
    [SerializeField] private int _mpMax = 20;
    [SerializeField] private int _mp;

    [Header("UI")]
    [SerializeField] private Image _hpBar;
    [SerializeField] private Image _mpBar;

    private void Start()
    {
        _hp = _hpMax;
        _mp = _mpMax;
    }

    //hp functions
    public int GetHP() {  return _hp; }
    public void SetHP(int _hp) {
        if (_hp > _hpMax)
            _hp = _hpMax;
        else if (_hp < 0)
            _hp = 0;
        this._hp = _hp;
    }
    public void TakeDamage(int _damage) {_hp -= _damage; }
    public void Heal(int _healPoints) { _hp += _healPoints; }

    //mp functions
    public int GetMP() { return _mp; }
    public void SetMP(int _mp) {
        if(_mp > _mpMax)
            _mp = _mpMax;
        else if(_mp < 0) 
            _mp = 0;
        this._mp = _mp; 
    }
    public void CastMagic(int _cost) { _mp -= _cost; }
    public void RestoreMagic(int _restaureMp) { _mp += _restaureMp; }
}
