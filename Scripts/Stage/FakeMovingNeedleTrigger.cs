using UnityEngine;

public class FakeMovingNeedleTrigger : MonoBehaviour
{
    [SerializeField] private FakeMovingNeedle targetObject;
    [SerializeField] private int triggerNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        targetObject.Activate(triggerNumber);
    }
}
