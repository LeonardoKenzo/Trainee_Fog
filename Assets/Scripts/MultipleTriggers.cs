using UnityEngine;
using System;

public class MultipleTriggers : MonoBehaviour
{
    //Events triggered by colliders
    public event Action<Collider2D> EnteredTrigger;
    public event Action<Collider2D> ExitedTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnteredTrigger?.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ExitedTrigger?.Invoke(collision);
    }
}
