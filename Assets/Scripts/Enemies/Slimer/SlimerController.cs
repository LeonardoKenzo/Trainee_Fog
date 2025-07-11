using System;
using UnityEngine;

public class SlimerController : BaseEnemy
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

    // References -------------------------
    public Rigidbody2D Rigidbody2D { get; private set; }

    // Events -----------------------------
    public event Action TurnDirection;
    protected override void Awake()
    {
        base.Awake();
        
        //Set the references

        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Colliders -------------------------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            TurnDirection?.Invoke();
        else if (collision.gameObject.CompareTag("Respawn"))
            Destroy(this.gameObject);
    }

    //IDamageDealer Functions ------------------------------
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        StartCoroutine(BlinkDamage());
    }
}
