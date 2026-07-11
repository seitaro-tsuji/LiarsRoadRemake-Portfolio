using UnityEngine;

public class Goal : MonoBehaviour
{
    Coroutine clearCoroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
    
        //クリア処理を開始
        if (clearCoroutine == null)
            clearCoroutine = StartCoroutine(StageManager.Instance.StageClearCoroutine());
    }
}
