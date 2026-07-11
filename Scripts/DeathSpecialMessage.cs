using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class DeathSpecialMessage : MonoBehaviour
{
    [SerializeField] private string deathSpecialMessage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーがこのオブジェクトにぶつかったら
        if (collision.CompareTag("Player"))
        {
            StageManager.Instance.SetDeathSpecialMessage(deathSpecialMessage);
        }
    }
}
