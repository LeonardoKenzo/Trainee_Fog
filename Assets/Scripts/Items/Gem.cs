using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private int _pointsValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PointsManager.Instance.AddPoints(_pointsValue);
            Destroy(gameObject);
        }
    }
}
