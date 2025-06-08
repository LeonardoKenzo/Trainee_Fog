using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void ResumeButton()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartButton()
    {
        Time.timeScale = 1f;
        MusicManager.PlayMusic(false);
        SceneManager.LoadScene(1);
    }

    public void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
