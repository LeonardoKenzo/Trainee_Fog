using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageCanvasAnimations : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private float _animationSpeed = 1f;

    private Coroutine _animationCoroutine;

    private void OnEnable()
    {
        _animationCoroutine = StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        int _currentFrame = 0;
        float _timer = 0f;
        float _frameDuration = 1f / _animationSpeed;

        while (true)
        {
            //the animation plays even if time.timeScale = 0
            _timer += Time.unscaledDeltaTime;

            if (_timer >= _frameDuration)
            {
                _timer -= _frameDuration;
                _image.sprite = _sprites[_currentFrame];
                _currentFrame = (_currentFrame + 1) % _sprites.Length;
            }

            //Wait for the next frame
            yield return null;
        }
    }

    private void OnDisable()
    {
        if(_animationCoroutine != null)
            StopCoroutine(Animation());
    }
}
