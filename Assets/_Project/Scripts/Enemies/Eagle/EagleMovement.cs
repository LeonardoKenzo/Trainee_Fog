using Unity.VisualScripting;
using UnityEngine;

public class EagleMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Rigidbody2D _rigidBody;
    [SerializeField] private float smoothTime = 0.2f;
    private Vector3 _moveSpeed = Vector3.zero; //calculated by SmoothDamp

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void FollowPlayer(GameObject _player)
    {
        if(_player != null)
        {
            Vector3 playerPosition = new Vector3(_player.transform.position.x, _player.transform.position.y + 1f, _player.transform.position.z);
            Vector3 newPosition = Vector3.SmoothDamp(transform.position, playerPosition, ref _moveSpeed, smoothTime);
            transform.localScale = new Vector3((transform.position.x > _player.transform.position.x)? 1: -1, 1, 1);

            _rigidBody.MovePosition(newPosition);
        }
    }
}
