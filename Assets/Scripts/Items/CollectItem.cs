using System.Collections;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        StartCoroutine(Collect());
    }

    private IEnumerator Collect()
    {
        _animator.Play("ClaimItem");
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
}
