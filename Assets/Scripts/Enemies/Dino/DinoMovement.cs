using UnityEngine;

public class DinoMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Rigidbody2D _enemyRigidbody2d;
    [SerializeField] private float _speed = 1.75f;
    [SerializeField] private float _direction = -1f;
    [SerializeField] private float _stunTime = 0f;

    [Header("Edge of the Ground Check")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheckTransform;

    [Header("Wall Check")]
    [SerializeField] private Transform _wallCheckTransform;
    [SerializeField] private Vector3 _wallCheckSize;
     
    [Header("Animation")]
    private Animator _animator;

    void Start()
    {
        _enemyRigidbody2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        //if the enemy is stunned stop the enemy
        if(_stunTime > 0f)
        {
            //Idle animation
            _animator.SetBool("isStunned", true);

            //make the enemy stop
            _enemyRigidbody2d.linearVelocity = Vector2.zero;
            _stunTime -= Time.deltaTime;
        }
        else
        {
            //run animation
            _animator.SetBool("isStunned", false);

            //moves the enemy
            _enemyRigidbody2d.linearVelocity = new Vector2(_direction * _speed, _enemyRigidbody2d.linearVelocityY);

            //if the enemy is at the edge of the platform or collide with the wall, flip the enemy
            bool _groundCheck = Physics2D.Raycast(_groundCheckTransform.position, Vector2.down, 1f, _groundLayer);
            bool _wallCheck = Physics2D.OverlapBox(_wallCheckTransform.position, _wallCheckSize, 0f, _groundLayer);
            if(!_groundCheck || _wallCheck)
                TurnDirection();
        }
    }

    //turn the direction of the enemy
    private void TurnDirection()
    {
        _direction *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    //when coliding with something
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            _stunTime = 1f;
    }
}
