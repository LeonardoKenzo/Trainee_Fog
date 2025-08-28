using UnityEngine;

public class NextLevelMenu : MonoBehaviour
{
    [SerializeField] private GameObject _nextLevelPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _nextLevelPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
