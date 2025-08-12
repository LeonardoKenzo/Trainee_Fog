using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerStatsManager _playerHealth;
    [SerializeField] private Image _currentHpBar;

    private void Update()
    {
        _currentHpBar.fillAmount = (_playerHealth.Hp / 10);
    }
}
