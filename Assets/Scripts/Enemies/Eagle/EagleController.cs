using System;
using System.Collections;
using UnityEngine;

public class EagleController : BaseEnemy
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

    // Variables ----------------------------------------
    private GameObject _player;
    private bool _isFollowing = false;

    // Scripts ------------------------------------------
    private EagleMovement _movement;

    // References ---------------------------------------
    public Rigidbody2D Rigidbody2D { get; private set; }

    [Header("Eagle Triggers")]
    [SerializeField] private MultipleTriggers _followTrigger;

    protected override void Awake()
    {
        base.Awake();

        //Set the References
        _movement = GetComponent<EagleMovement>();

        Rigidbody2D = GetComponent<Rigidbody2D>();

        //Set events on Trigger Collider
        if(_followTrigger == null)
        {
            _followTrigger = GetComponentInChildren<MultipleTriggers>();
        }
        _followTrigger.EnteredTrigger += OnFollowTriggerEnter;
        _followTrigger.ExitedTrigger += OnFollowTriggerExit;
    }

    private void Update()
    {
        if (_isFollowing)
        {
            _movement.FollowPlayer(_player);
        }
    }

    // Functions and Coroutines ------------------------------

    //Add blink effect when receive damage
    public override IEnumerator BlinkDamage()
    {
        float _elapsed = 0f;
        float _blinkInterval = 0.1f;
        bool _isTransparent = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        _isFollowing = false;

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

        _isFollowing = true;
    }

    private IEnumerator StopFollowPlayer(float delay)
    {
        yield return new WaitForSeconds(delay);
        _player = null;
        _isFollowing = false;
    }

    // OnTriggerEnter and OnTriggerExit------------------------------
    private void OnFollowTriggerEnter(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            if (!_isFollowing)
            {
                _player = collision.gameObject;
                _isFollowing = true;
            }
        }
    }
    private void OnFollowTriggerExit(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(StopFollowPlayer(2f));
        }
    }

    // BaseEnemy Functions ---------------------------------------

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        StartCoroutine(BlinkDamage());
    }
}
