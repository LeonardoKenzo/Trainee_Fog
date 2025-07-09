using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DinoController : BaseEnemy
{
    /*
     *  Inherited from BaseEnemy:
     *  - [SerializeField] GameObject _deathObject;
     *  - [SerializeField] EnemiesStatsSO _statsSO;
     *  - EnemiesRuntimeStats _stats;
     *  
     *  - public void TakeDamage(float damage);
     *  - public int GetDamage();
     *  - public void KillEnemy()
     */

    // Movement------------------
    private DinoMovement _dinoMovement;

    // References ---------------
    public Rigidbody2D Rigidbody2D { get; private set; }
    public Animator Animator { get; private set; }

    private void Awake()
    {
        //Initialize the dino stats
        _stats = new EnemiesRuntimeStats(_statsSO);

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
    }

    // Functions ---------------------------------------------------------

    private IEnumerator BlinkDamage()
    {
        float _elapsed = 0f;
        float _blinkInterval = 0.1f;
        bool _isTransparent = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        while (_elapsed < 0.5f)
        {
            Color transparence = spriteRenderer.color;

            if (_isTransparent)
            {
                //Turns Invisible
                transparence.a = 0f;
                spriteRenderer.color = transparence;
                _isTransparent = false;
            }
            else if (!_isTransparent)
            {
                //Turns Visible
                transparence.a = 1f;
                spriteRenderer.color = transparence;
                _isTransparent = true;
            }

            yield return new WaitForSeconds(_blinkInterval);

            _elapsed += _blinkInterval;
        }

        //Guarantee the visibility of sprite 
        Color finalColor = spriteRenderer.color;
        finalColor.a = 1f;
        spriteRenderer.color = finalColor;
        _isTransparent = false;
    }

    // BaseEnemy Functions ----------------------------------------
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        StartCoroutine(BlinkDamage());
    }
}
