using System;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Horizontal Movement")]
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private float _moveSpeed = 7f;
    private float _horizontalMovement;

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 5f;
    private float _jumpsRemaining;
    private float _totalJumps = 2;

    [Header("Dash")]
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashStun;
    [SerializeField] private float _minDashSpeed;
    [SerializeField] private float _dashMultiplier;
    private bool _isDashing = false;

    [Header("Ground Check")]
    [SerializeField] private float _groundCheckPositionHeight;
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.5f, 0.05f);
    [SerializeField] private LayerMask _groundLayer;

    [Header("Gravity")]
    [SerializeField] private float _gravityMultiplier = 2f;
    [SerializeField] private float _maxGravityScale = 18f;
    private float _normalGravityForce = 1f;

    [Header("Stun")]
    [SerializeField] private float _hitStun = 1f;
    private bool _isTakingDamage;
    private float _stunnedTime = 0f;

    [Header("Stats")]
    [SerializeField] private PlayerStatsManager _statsManager;

    [Header("Animation")]
    private Animator _animator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //the player is stunned and pushed back
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _stunnedTime = _hitStun;
            _animator.SetBool("IsHurt", true);
            _isTakingDamage = true;
            rigidbody2d.linearVelocity = Vector2.zero;
            rigidbody2d.gravityScale = _normalGravityForce;
            rigidbody2d.AddForce(new Vector2((collision.gameObject.transform.position.x > transform.position.x ? -1 : 1) * 4f, 0f), ForceMode2D.Impulse);
            _statsManager.TakeDamage(1);
        }
    }

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();  
        _statsManager = GetComponent<PlayerStatsManager>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        //is is stunned, stop moving normaly
        if(_stunnedTime > 0f)
        {
            //the player is stopped by friction with the floor
            if (GroundCheck() && !_isDashing)
                rigidbody2d.linearDamping = 3f;

            //Makes the dash smoother and priorize the enemy hit interaction
            if (!_isTakingDamage)
                DashNormalize((int)transform.localScale.x);
            else
                _isDashing = false;

            _stunnedTime -= Time.deltaTime;
        }
        else
        {
            _isTakingDamage = false;
            _animator.SetBool("IsHurt", false);

            if (GroundCheck() && _horizontalMovement != 0)
                _animator.SetBool("IsMoving", true);
            else
                _animator.SetBool("IsMoving", false);

            //moves the player
            rigidbody2d.linearVelocity = new Vector2(_horizontalMovement * _moveSpeed, rigidbody2d.linearVelocityY);
            rigidbody2d.linearDamping = 0f;
            GravityIncrease();

            if (rigidbody2d.linearVelocityY > 0)
                _animator.SetFloat("VelocityY", 1);

            //flip the sprite player
            if (_horizontalMovement > 0)
                transform.localScale = new Vector2(1f, transform.localScale.y);
            else if (_horizontalMovement < 0)
                transform.localScale = new Vector2(-1f, transform.localScale.y);

            //check if player is on the ground and reset jumps adn dash if is
            if (GroundCheck())
            {
                _jumpsRemaining = _totalJumps;
                _isDashing = false;
            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        _horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        //if hold down the jump button = full jump force
        if (context.performed && _jumpsRemaining > 0 && !_isTakingDamage)
        {
            rigidbody2d.linearVelocityY = _jumpForce;
            _jumpsRemaining--;
        }
        //if light tap the jump button = half the jump
        else if (context.canceled && _jumpsRemaining  > 0 && !_isTakingDamage)
        {
            rigidbody2d.linearVelocityY = rigidbody2d.linearVelocityY * 0.5f;
            _jumpsRemaining--;
        }
    }

    //Dash Function
    public void Dash(InputAction.CallbackContext context)
    {
        //if the player is not dashing and is not taking damage, can dash
        if(context.performed && !_isDashing && !_isTakingDamage)
        {
            _stunnedTime = _dashStun;
            rigidbody2d.gravityScale = 0;
            rigidbody2d.linearVelocity = new Vector2(transform.localScale.x * _dashSpeed, 0f);
            _isDashing = true;
            _animator.Play("PlayerStartJump", 0, 0f);
        }
    }

    //function to check if the player is touching the ground
    private bool GroundCheck()
    {
        if (Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - _groundCheckPositionHeight), _groundCheckSize, 0, _groundLayer))
            return true;
        else
            return false;
    }

    //increases the fall of the player
    private void GravityIncrease()
    {
        if(rigidbody2d.linearVelocityY < 0 && rigidbody2d.gravityScale < _maxGravityScale)
        {
            rigidbody2d.gravityScale *= _gravityMultiplier;
            _animator.SetFloat("VelocityY", -1);
            if (rigidbody2d.gravityScale >= _maxGravityScale)
                rigidbody2d.gravityScale = _maxGravityScale;
        }
        else
        {
            rigidbody2d.gravityScale = _normalGravityForce;
        }
    }

    //makes the dash smoother
    private void DashNormalize(int direction)
    {
        if (rigidbody2d.linearVelocityX != 0 && Math.Abs(rigidbody2d.linearVelocityX) > Math.Abs(_minDashSpeed) && _isDashing)
        {
            rigidbody2d.linearVelocityX *= _dashMultiplier;
            if (Math.Abs(rigidbody2d.linearVelocityX) <= Math.Abs(_minDashSpeed))
                rigidbody2d.linearVelocityX = _minDashSpeed * direction;
        }
        else
            rigidbody2d.linearVelocityX = _minDashSpeed * direction;
    }

    //draw a gizmo in scene to check if the player is touching the ground
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y - _groundCheckPositionHeight), _groundCheckSize);
    }
}
