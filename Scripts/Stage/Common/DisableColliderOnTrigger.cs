using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))] 
public class DisableColliderOnTrigger : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";

    private bool triggerd;
    private Collider2D triggerCollider; 

    private void Awake()
    {
        triggerd = false;
        triggerCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //既に検知済みなら終了
        if (triggerd)
            return;

        //ターゲット以外のコライダーなら無視
        if (!collision.CompareTag(targetTag))
            return;

        triggerd = true;
        StartCoroutine(DisableColliderCoroutine());
    }

    private IEnumerator DisableColliderCoroutine()
    {
        //次フレームでコライダーを無効化する
        yield return null;
        triggerCollider.enabled = false;
    }
}
