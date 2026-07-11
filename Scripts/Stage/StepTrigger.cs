using UnityEngine;
using UnityEngine.Events;

public class StepTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent onStepped;

    private bool isActivated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActivated) return;

        if (collision.CompareTag("Player"))
        {
            isActivated = true;
            onStepped?.Invoke();
        }
    }
}
