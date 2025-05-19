using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movimentacao horizontal")]
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private float _moveSpeed = 7f;
    private float _horizontalMovement;


    [Header("Pulo")]
    [SerializeField] private float _jumpForce = 5f;
    private float _jumpsRemaining;
    private float _totalJumps = 2;

    [Header("Ground Check")]
    [SerializeField] private float _groundCheckPositionHeight;
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.5f, 0.05f);
    [SerializeField] private LayerMask _groundLayer;

    [Header("Gravity")]
    [SerializeField] private float _normalGravityForce = 1f;
    [SerializeField] private float _gravityMultiplier = 2f;
    [SerializeField] private float _maxGravityScale = 18f;

    private void Start()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        //moves the player
        rigidbody2d.linearVelocity = new Vector2(_horizontalMovement * _moveSpeed, rigidbody2d.linearVelocityY);
        GravityIncrease();

        //flip the sprite player
        if (_horizontalMovement > 0)
            transform.localScale = new Vector2(1f, transform.localScale.y);
        else if (_horizontalMovement < 0)
            transform.localScale = new Vector2(-1f, transform.localScale.y);

        //check if player is on the ground
        GroundCheck();

    }

    public void Move(InputAction.CallbackContext context)
    {
        _horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        //if hold down the jump button = full jump force
        if (context.performed && _jumpsRemaining > 0)
        {
            rigidbody2d.linearVelocityY = _jumpForce;
            _jumpsRemaining--;
        }
        //if light tap the jump button = half the jump
        else if (context.canceled && _jumpsRemaining  > 0)
        {
            rigidbody2d.linearVelocityY = rigidbody2d.linearVelocityY * 0.5f;
            _jumpsRemaining--;
        }
    }

    //function to check if the player is touching the ground
    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - _groundCheckPositionHeight), _groundCheckSize, 0, _groundLayer))
        {
            //reset the amount of jumps
            _jumpsRemaining = _totalJumps;
        }
    }

    //increases the fall of the player
    private void GravityIncrease()
    {
        if(rigidbody2d.linearVelocityY < 0 && rigidbody2d.gravityScale < _maxGravityScale)
        {
            rigidbody2d.gravityScale *= _gravityMultiplier;
            if(rigidbody2d.gravityScale >= _maxGravityScale)
                rigidbody2d.gravityScale = _maxGravityScale;
        }
        else
        {
            rigidbody2d.gravityScale = _normalGravityForce;
        }
    }


    //draw a gizmo in scene to check if the player is touching the ground
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y - _groundCheckPositionHeight), _groundCheckSize);
    }
}
