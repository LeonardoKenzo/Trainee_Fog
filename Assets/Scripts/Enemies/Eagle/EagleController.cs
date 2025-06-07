using System.Collections;
using UnityEngine;

public class EagleController : MonoBehaviour,IDamageDealer
{
    [Header("Eagle Stats")]
    [SerializeField] private EnemiesStatsSO _statsSO;
    [SerializeField] private EnemiesRuntimeStats _stats;

    // Variables ----------------------------------------
    private GameObject _player;
    private bool _isFollowing = false;

    // Scripts ------------------------------------------
    private EagleMovement _movement;

    // References ---------------------------------------
    public Rigidbody2D Rigidbody2D { get; private set; }


    // Follow the player if it enters in the follow zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player = collision.gameObject;
            _isFollowing = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player = null;
            _isFollowing = false;
        }
    }

    void Awake()
    {
        //initialize the stats
        _stats = new EnemiesRuntimeStats(_statsSO);

        _movement = GetComponent<EagleMovement>();

        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isFollowing)
        {
            _movement.FollowPlayer(_player);
        }
    }

    // Functions -------------------------------------
    public void TakeDamage(float damage)
    {
        _stats.CurrentHP -= damage;
        StartCoroutine(BlinkDamage());
        //Die and add Points
        if (_stats.CurrentHP <= 0)
        {
            PointsManager.Instance.AddPoints(_stats.PointsValue);
            Destroy(gameObject);
        }
    }

    private IEnumerator BlinkDamage()
    {
        float _elapsed = 0f;
        float _blinkInterval = 0.1f;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        while (_elapsed < 0.5f)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(_blinkInterval);
            _elapsed += _blinkInterval;
        }

        spriteRenderer.enabled = true;
    }

    public int GetDamage()
    {
        return _stats.BaseDamage;
    }

}
