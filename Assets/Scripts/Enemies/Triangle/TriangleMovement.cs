using UnityEngine;

public class TriangleMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Rigidbody2D _enemyRigidbody2d;
    [SerializeField] private float _speed = 1.75f;
    [SerializeField] private float _direction = 1f;

    [Header("Edge of the Ground Check")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _checkPosition;
    

    void Start()
    {
        _enemyRigidbody2d = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //moves the enemy
        _enemyRigidbody2d.linearVelocity = new Vector2(_direction * _speed, _enemyRigidbody2d.linearVelocityY);

        //if the enemy is at the edge of the platform, flip the enemy
        RaycastHit2D _groundCheck = Physics2D.Raycast(_checkPosition.position, Vector2.right, 1f, _groundLayer);
        if(_groundCheck.collider == null)
            TurnDirection();
    }

    //turn the direction of the enemy
    private void TurnDirection()
    {
        _direction *= -1;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
    }

    //when coliding with something, turn enemy Direction
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            TurnDirection();
    }
}
