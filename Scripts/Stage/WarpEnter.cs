using System.Collections;
using UnityEngine;

public class WarpEnter : MonoBehaviour
{
    [SerializeField] private Transform warpExit;

    private Coroutine warpCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (warpCoroutine != null)
            return;

        warpCoroutine = StartCoroutine(WarpRoutine());
    }

    private IEnumerator WarpRoutine()
    {
        yield return StageManager.Instance.PlayerWarp(warpExit.position);
        warpCoroutine = null;
    }
}
