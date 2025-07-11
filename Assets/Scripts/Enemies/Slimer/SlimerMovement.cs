using System;
using System.Collections;
using UnityEngine;

public class SlimerMovement : MonoBehaviour
{
    // Controller Reference ---------------------------
    private SlimerController _controller;

    [Header("Movement Jump")]
    [SerializeField] private float _jumpHigh;
    [SerializeField] private float _jumpDistance;
    private Rigidbody2D _rigidbody;
    [SerializeField] private bool _isJumping;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheckTransform;
    [SerializeField] private Vector2 _groundCheckSize;
    private LayerMask _groundLayer;
    [SerializeField] private bool _isGrounded;

    [Header("Hit Object")]
    [SerializeField] private Transform _hitCheckTransform;
    [SerializeField] private Vector2 _hitCheckSize;
    private LayerMask _hitLayer;

    private void Start()
    {
        _controller = GetComponent<SlimerController>();

        _rigidbody = _controller.Rigidbody2D;
        _controller.TurnDirection += TurnDirection;

        _groundLayer = LayerMask.GetMask("Ground", "Platform");
        _hitLayer = LayerMask.GetMask("Ground", "Player");
    }

    private void FixedUpdate()
    {
        //Check if the slimer is grounded
        _isGrounded = Physics2D.OverlapBox(_groundCheckTransform.position, _groundCheckSize, 0f, _groundLayer);

        //Check if the slimer hit something
        if(Physics2D.OverlapBox(_hitCheckTransform.position, _hitCheckSize, 0f, _hitLayer))
        {
            TurnDirection();
        }
    }

    private void Update()
    {
        if (_isGrounded && !_isJumping)
        {
            StartCoroutine(Jump());
        }
    }

    // Functions and Coroutines ---------------------------------------------------------------

    private IEnumerator Jump()
    {
        _isJumping = true;
        
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1f));

        _rigidbody.AddForce(new Vector2(_jumpDistance, _jumpHigh), ForceMode2D.Impulse);

        //Wait for the slimer jump and get off the ground
        yield return new WaitWhile(() => _isGrounded);

        //Wait for the slimer touch the ground
        yield return new WaitUntil(() => _isGrounded);

        _isJumping = false;
    }

    private void TurnDirection()
    {
        _jumpDistance *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); 
    }
}
