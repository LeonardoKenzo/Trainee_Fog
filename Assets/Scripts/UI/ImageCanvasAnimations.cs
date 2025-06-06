using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageCanvasAnimations : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private float _animationSpeed = 1f;

    public void StartAnimation()
    {
        StartCoroutine(Animation());
    }
    private IEnumerator Animation()
    {
        int currentFrame = 0;
        while (true)
        {
            _image.sprite = _sprites[currentFrame];
            currentFrame = (currentFrame + 1) % _sprites.Length;
            yield return new WaitForSeconds(1f / _animationSpeed);
        }
    }
    public void StopAnimation()
    {
        StopCoroutine(Animation());
    }
}
