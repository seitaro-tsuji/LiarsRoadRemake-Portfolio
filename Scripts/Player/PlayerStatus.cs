using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(PlayerController))]
public class PlayerStatus : MonoBehaviour
{
    private PlayerController _controller;
    private Animator animator;
    private CapsuleCollider2D _collider;
    private bool isDead;

    //死亡時のイベント　　ただ、処理の流れが見えなくなるので、今後イベントハブを作って改善するかも
    public UnityEvent OnPlayerDead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _controller = GetComponent<PlayerController> ();
        animator = GetComponent<Animator> ();
        animator.SetBool("Die", false);
        _collider = GetComponent<CapsuleCollider2D>();
        isDead = false;
    }

    //死亡処理
    public void OnDie(string deathCause)
    {
        if (isDead) return;

        isDead = true;

        _controller.DisableControl();   
        _controller.MoveStop();
        animator.SetTrigger("Die");     
        StageManager.Instance.SetDeathCause(deathCause); //死因を登録
        AudioManager.Instance.PlaySE("die");

        //イベントを使う(開発途中で知ったのでイベントを使っていない実装部分もあるが)
        OnPlayerDead?.Invoke();

        //コライダー無効化
        if (_collider != null)
        {
            _collider.enabled = false;
        }
    }
}
