using UnityEngine;

public class Cherry : MonoBehaviour
{
    [SerializeField] private float _healAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            PlayerStatsManager _playerStats = collision.GetComponent<PlayerStatsManager>();
            _playerStats.Heal(_healAmount);
            Destroy(gameObject);
        }
    }
}
