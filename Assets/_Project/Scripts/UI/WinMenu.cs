using UnityEngine;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _winPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
