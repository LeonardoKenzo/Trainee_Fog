using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private int _pointsValue = 1;
    [SerializeField] private GameObject _collectAnim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PointsManager.Instance.AddPoints(_pointsValue);

            Instantiate(_collectAnim, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
