using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        AnimatorStateInfo animationInfo = _animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = animationInfo.length;

        StartCoroutine(Die(animationDuration));
    }

    private IEnumerator Die(float delay)
    {
        _animator.Play("EnemyDeath");
        yield return new WaitForSeconds(delay + 0.1f);
        Destroy(gameObject);
    }
}
