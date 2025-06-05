using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private float _parallaxEffect;
    private float _startPosition;
    private float _length;

    void Start()
    {
        _startPosition = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float distance = _camera.transform.position.x * _parallaxEffect;
        float movement = _camera.transform.position.x * (1 - _parallaxEffect);

        transform.position = new Vector3(_startPosition + distance, _camera.transform.position.y, transform.position.z);

        if (movement > _startPosition + _length)
            _startPosition += _length;
        else if(movement < _startPosition - _length)
            _startPosition -= _length;
    }
}