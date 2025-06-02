using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartMenu : MonoBehaviour
{
    //Buttons -----------------------------------
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OptionsButton()
    {
        return;
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    //Image animation ---------------------------

    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private float _animationSpeed = 1f;

    private void Start()
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

}
