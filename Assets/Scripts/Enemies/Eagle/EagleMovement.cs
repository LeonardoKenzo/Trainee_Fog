using UnityEngine;

public class EagleMovement : MonoBehaviour
{
    private EagleController _controller;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    private Rigidbody2D _rigidbody2d;

    
    void Start()
    {
        _controller = GetComponent<EagleController>();

        //Initialize the references
        _rigidbody2d = _controller.Rigidbody2D;
    }

    void Update()
    {
        
    }
}
