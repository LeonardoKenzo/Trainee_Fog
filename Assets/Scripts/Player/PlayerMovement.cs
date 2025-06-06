using System;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController _playerController;

    [Header("Horizontal Movement")]
    [SerializeField] private Rigidbody2D _rigidbody2d;
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
    private bool _groundCheck;

    [Header("Gravity")]
    [SerializeField] private float _gravityMultiplier = 2f;
    [SerializeField] private float _maxGravityScale = 18f;
    private float _normalGravityForce = 1f;

    [Header("Stun")]
    [SerializeField] private float _hitStun = 1f;
    private bool _isTakingDamage;
    private float _stunnedTime = 0f;

    [Header("Animation")]
    private Animator _animator;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _rigidbody2d = _playerController.Rigidbody2D;
        _animator = _playerController.Animator;
    }

    private void FixedUpdate()
    {
        //make the ground check
        _groundCheck = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - _groundCheckPositionHeight), _groundCheckSize, 0, _groundLayer);
    }

    void Update()
    {
        //is is stunned, stop moving normaly
        if(_stunnedTime > 0f)
        {
            //the player is stopped by friction with the floor
            if (GroundCheck() && !_isDashing)
                _rigidbody2d.linearDamping = 3f;

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

            //animation
            if (_rigidbody2d.linearVelocityY >= -0.5 && _rigidbody2d.linearVelocityY <= 0.5 && _horizontalMovement != 0 && GroundCheck())
                _animator.Play("PlayerRun");
            else if(_rigidbody2d.linearVelocityY >= -0.5 && _rigidbody2d.linearVelocityY <= 0.5 && _horizontalMovement == 0 && GroundCheck())
                _animator.Play("PlayerIdle");

            //moves the player
            _rigidbody2d.linearVelocity = new Vector2(_horizontalMovement * _moveSpeed, _rigidbody2d.linearVelocityY);
            _rigidbody2d.linearDamping = 0f;
            GravityIncrease();

            //flip the sprite player
            if (_horizontalMovement > 0)
                transform.localScale = new Vector2(1f, transform.localScale.y);
            else if (_horizontalMovement < 0)
                transform.localScale = new Vector2(-1f, transform.localScale.y);

            //check if player is on the ground and reset jumps and dash if is
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
        if(_jumpsRemaining > 0 && !_isTakingDamage)
            _animator.Play("PlayerStartJump", 0, 0f);
        //if hold down the jump button = full jump force
        if (context.performed && _jumpsRemaining > 0 && !_isTakingDamage)
        {
            _rigidbody2d.linearVelocityY = _jumpForce;
            _jumpsRemaining--;
        }
        //if light tap the jump button = half the jump
        else if (context.canceled && _jumpsRemaining  > 0 && !_isTakingDamage)
        {
            _rigidbody2d.linearVelocityY = _rigidbody2d.linearVelocityY * 0.5f;
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
            _rigidbody2d.gravityScale = 0;
            _rigidbody2d.linearVelocity = new Vector2(transform.localScale.x * _dashSpeed, 0f);
            _isDashing = true;
            _animator.Play("PlayerStartJump", 0, 0f);
        }
    }

    //function to check if the player is touching the ground
    private bool GroundCheck()
    {
        if (_groundCheck)
            return true;
        else
            return false;
    }

    //increases the fall of the player
    private void GravityIncrease()
    {
        if(_rigidbody2d.linearVelocityY < 0 && _rigidbody2d.gravityScale < _maxGravityScale)
        {
            _rigidbody2d.gravityScale *= _gravityMultiplier;
            if(!GroundCheck())
                _animator.Play("PlayerEndJump");
            if (_rigidbody2d.gravityScale >= _maxGravityScale)
                _rigidbody2d.gravityScale = _maxGravityScale;
        }
        else
        {
            _rigidbody2d.gravityScale = _normalGravityForce;
        }
    }

    //makes the dash smoother
    private void DashNormalize(int direction)
    {
        if (_rigidbody2d.linearVelocityX != 0 && Math.Abs(_rigidbody2d.linearVelocityX) > Math.Abs(_minDashSpeed) && _isDashing)
        {
            _rigidbody2d.linearVelocityX *= _dashMultiplier;
            if (Math.Abs(_rigidbody2d.linearVelocityX) <= Math.Abs(_minDashSpeed))
                _rigidbody2d.linearVelocityX = _minDashSpeed * direction;
        }
        else
            _rigidbody2d.linearVelocityX = _minDashSpeed * direction;
    }

    public void EnemyHit(Collision2D collision)
    {
        _stunnedTime = _hitStun;
        _animator.Play("PlayerHurt", 0, 0f);
        _isTakingDamage = true;
        _rigidbody2d.linearVelocity = Vector2.zero;
        _rigidbody2d.gravityScale = _normalGravityForce;
        _rigidbody2d.AddForce(new Vector2((collision.gameObject.transform.position.x > transform.position.x ? -1 : 1) * 4f, 0f), ForceMode2D.Impulse);
    }
}
