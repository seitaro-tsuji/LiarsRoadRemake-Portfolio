using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MovingObjectTrigger : MonoBehaviour
{
    [SerializeField] private MovingObject movingObject;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        movingObject?.Activate(_collider);  //プレイヤーに触れたコライダーをMovingObjectに通知する
    }
}
