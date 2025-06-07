using UnityEngine;

public class DinoMovement : MonoBehaviour
{
    private DinoController controller;

    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _direction = -1f;
    [SerializeField] private float _stunTime;
    private Rigidbody2D _enemyRigidbody2d;

    [Header("Edge of the Ground Check")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheckTransform;
    private bool _groundCheck;

    [Header("Wall Check")]
    [SerializeField] private Transform _wallCheckTransform;
    [SerializeField] private Vector3 _wallCheckSize;
    private bool _wallCheck;
     
    // Animation ----------------
    private Animator _animator;


    void Start()
    {
        controller = GetComponent<DinoController>();

        //Initialize the references
        _enemyRigidbody2d = controller.Rigidbody2D;
        _animator = controller.Animator;
    }

    private void FixedUpdate()
    {
        _groundCheck = Physics2D.Raycast(_groundCheckTransform.position, Vector2.down, 1f, _groundLayer);
        _wallCheck = Physics2D.OverlapBox(_wallCheckTransform.position, _wallCheckSize, 0f, _groundLayer);
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
            if(!_groundCheck || _wallCheck)
                TurnDirection();
        }
    }

    //allow the controller script acess and change these variables
    public float StunTime
    {
        set { _stunTime = value; }
    }

    public float MoveSpeed
    {
        set { _speed = value; }
    }


    //turn the direction of the enemy
    private void TurnDirection()
    {
        _direction *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
