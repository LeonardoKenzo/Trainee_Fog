using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Scripts")]
    private PlayerStatsManager _playerStats;
    private PlayerMovement _playerMovement;

    [Header("References")]
    public Animator Animator {  get; private set; }
    public Rigidbody2D Rigidbody2D { get; private set; }

    private void Awake()
    {
        //initialize the references
        _playerMovement = GetComponent<PlayerMovement>();
        _playerStats = GetComponent<PlayerStatsManager>();

        Animator = GetComponent<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //the player is stunned and pushed back
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _playerMovement.EnemyHit(collision);
            int damage = collision.gameObject.GetComponent<IDamageDealer>().GetDamage();

            _playerStats.TakeDamage(damage);
        }
            
    }
}
