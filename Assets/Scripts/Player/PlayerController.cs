using System.Collections;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables ---------------------------------------
    private bool _isInvulnerable = false;

    // Scripts -----------------------------------------
    private PlayerStatsManager _playerStats;
    private PlayerMovement _playerMovement;

    // References --------------------------------------
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (_isInvulnerable) { return; }

            _playerMovement.EnemyHit(collision);
            int damage = collision.gameObject.GetComponent<IDamageDealer>().GetDamage();

            _playerStats.TakeDamage(damage);

            StartCoroutine(Invulnerability());
        } 
        else if (collision.gameObject.CompareTag("Respawn"))
        {
            _playerStats.Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyHead"))
        {
            collision.gameObject.GetComponentInParent<IDamageDealer>().TakeDamage(2f);
            _playerMovement.JumpAttack();
        }
    }

    // cant take damage
    private  IEnumerator Invulnerability()
    {
        _isInvulnerable = true;

        yield return new WaitForSeconds(1f);

        _isInvulnerable = false;
    }
}
