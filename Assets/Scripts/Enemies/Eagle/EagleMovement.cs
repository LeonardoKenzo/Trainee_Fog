using Unity.VisualScripting;
using UnityEngine;

public class EagleMovement : MonoBehaviour
{
    private EagleController _controller;

    [Header("Movement")]
    private Vector3 _moveSpeed; //calculated by SmoothDamp
    
    private void Start()
    {
        _controller = GetComponent<EagleController>();
    }

    public void FollowPlayer(GameObject _player)
    {
        if(_player != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(_player.transform.position.x, _player.transform.position.y + 1f, _player.transform.position.z), ref _moveSpeed, 1.5f);
            transform.localScale = new Vector3((transform.position.x > _player.transform.position.x)? 1: -1, 1, 1);
        }
    }
}
