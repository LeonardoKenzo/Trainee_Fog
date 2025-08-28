using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    bool _isPaused = false;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !_isPaused)
        {
            Pause();
        }
        else if(Input.GetKeyUp(KeyCode.Escape) && _isPaused)
        {
            ResumeButton();
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
        PointsManager.Instance.RestartPoints();
        MusicManager.PlayMusic(false);
        SceneManager.LoadScene(1);
    }

    public void NextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextScene);
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
