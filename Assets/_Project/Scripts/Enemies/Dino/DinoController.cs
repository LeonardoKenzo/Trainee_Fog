using System;
using System.Collections;
using UnityEngine;

public class DinoController : BaseEnemy
{
    /*
     *  Inherited from BaseEnemy:
     *  - [SerializeField] GameObject _deathObject;
     *  - [SerializeField] EnemiesStatsSO _statsSO;
     *  - EnemiesRuntimeStats _stats; (All the stats of the enemy)
     *  
     *  - public void TakeDamage(float damage);
     *  - public int GetDamage();
     *  - public void KillEnemy()
     *  - public IEnumerator BlinkDamage();
     */

    // Scripts ------------------
    private DinoMovement _dinoMovement;

    // References ---------------
    public Rigidbody2D Rigidbody2D { get; private set; }
    public Animator Animator { get; private set; }

    // Events ----------------------
    public event Action TurnDirection;

    protected override void Awake()
    {
        base.Awake();

        //Initialize the references
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

        //Initialize the child scripts
        _dinoMovement = GetComponent<DinoMovement>();

        //Set the move speed of the dino
        _dinoMovement.MoveSpeed = _stats.MoveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            _dinoMovement.StunTime = 1f;
        else if (collision.gameObject.CompareTag("Enemy"))
            TurnDirection?.Invoke();
    }

    // Functions -------------------------------------------------
    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    // BaseEnemy Functions ----------------------------------------
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        StartCoroutine(BlinkDamage());
    }
}
