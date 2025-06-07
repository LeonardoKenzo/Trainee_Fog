using System.Collections;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    [SerializeField] private float _healAmount;
    [SerializeField] private GameObject _collectAnim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStatsManager _playerStats = collision.GetComponent<PlayerStatsManager>();
            _playerStats.Heal(_healAmount);
            Instantiate(_collectAnim, transform);
            Destroy(gameObject);
        }
    }
}
