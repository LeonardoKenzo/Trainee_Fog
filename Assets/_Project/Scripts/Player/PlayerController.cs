using System.Collections;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables ---------------------------------------
    private bool _isInvulnerable = false;

    // Scripts -----------------------------------------
    private PlayerMovement _playerMovement;
    private PlayerStatsManager _playerStats;

    // References --------------------------------------
    public Animator Animator {  get; private set; }
    public Rigidbody2D Rigidbody2D { get; private set; }

    private void Awake()
    {
        //Initialize the references
        _playerMovement = GetComponent<PlayerMovement>();
        _playerStats = GetComponent<PlayerStatsManager>();

        Animator = GetComponent<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();

        //Guarantee the collision
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Player"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("FlyEnemy"), LayerMask.NameToLayer("Player"), false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Collide with an Enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (_isInvulnerable) { return; }

            //Enemy damage the player 
            _playerMovement.EnemyHit(collision);
            int damage = collision.gameObject.GetComponent<IDamageDealer>().GetDamage();

            _playerStats.TakeDamage(damage);

            //Become invulnerable for a short period of time
            StartCoroutine(Invulnerability());
        } 
        //Falls of the map
        else if (collision.gameObject.CompareTag("Respawn"))
        {
            _playerStats.Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Jump in the enemy head
        if (collision.CompareTag("EnemyHead"))
        {
            //Do damage in enemy
            collision.gameObject.GetComponentInParent<IDamageDealer>().TakeDamage(_playerStats.Damage);
            _playerMovement.JumpAttack();
        }
    }
    // Functions and Coroutines --------------------------------------

    //Can't take damage
    private  IEnumerator Invulnerability()
    {
        _isInvulnerable = true;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Player"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("FlyEnemy"), LayerMask.NameToLayer("Player"), true);

        yield return new WaitForSeconds(1.5f);

        _isInvulnerable = false;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Player"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("FlyEnemy"), LayerMask.NameToLayer("Player"), false);
    }
}
