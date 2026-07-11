using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("StartPoint")]
    [SerializeField] private Transform startPoint;  //最初のスタート地点

    [Header("Jump Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpPower = 8f;
    [SerializeField] private float jumpHoldSecMax = 0.2f; //ジャンプ長押しできる最大秒数
    [SerializeField] private float coyoteTime = 0.1f;       //落下しているときのジャンプ猶予時間
    [SerializeField] private float maxFallSpeed = 15f;      //落下速度の最大

    [Header("Player Head")]
    [SerializeField] private GameObject headCollider;   //頭の判定

    //地面判定用
    [Header("Player Foot")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius = 0.1f;
    [SerializeField] private LayerMask jumpableLayer;

    private InputAction _move;
    private InputAction _jump;
    private Vector2 moveValue;  //moveの入力取得
    private bool jumpRequest;   //jump入力探知
    private bool jumpRaising;       //jumpで上昇中ならtrue

    private Animator animator;    //アニメーター

    private SpriteRenderer renderer;    //画像

    private float jumpSecCnt;   //ジャンプ開始から何秒経ったか
    private float coyoteTimer;  //ジャンプ猶予時間カウントダウン

    //ここにプレイヤーの無敵判定を追加するかも

    public Rigidbody2D Rb { get; private set; }  //これでキャラを動かす
    public Vector2 Velocity
    {
        get => Rb.linearVelocity;
        private set
        {
            Rb.linearVelocity = value;

            //速度を変更したらアニメーターにもそれを送る
            animator.SetFloat("HoriSpeed",Mathf.Abs(value.x));
            animator.SetFloat("VertSpeed", value.y);
        }
    }

    public bool IsControllable { get; private set; } //操作可能かどうか

    void Start()
    {
        //playerinputの有効化
        var input = GetComponent<PlayerInput>();
        input.currentActionMap.Enable();

        _move = input.currentActionMap.FindAction("Move");
        _jump = input.currentActionMap.FindAction("Jump");

        //アニメーター
        animator = GetComponent<Animator>();

        Rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();

        IsControllable = true;
    }

    void Update()
    {
        if (!IsControllable)
            return;

        //移動入力取得
        moveValue = _move.ReadValue<Vector2>();

        //地上、空中にいるときの処理
        if (OnGround())
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        //coyoteTimeの中でジャンプボタンを押したとき
        if (_jump.WasPressedThisFrame() && coyoteTimer > 0f)
        {
            jumpRequest = true;
            jumpRaising = true;
            jumpSecCnt = 0f;
            coyoteTimer = 0f;
            AudioManager.Instance.PlayJumpSE();
        }

        //jump長押し中
        if (jumpRaising)
        {
            //ジャンプボタン長押しかつジャンプ継続できる時間内なら
            if (_jump.IsPressed() && CanContinueJump())
            {
                jumpRequest = true;
                jumpSecCnt += Time.deltaTime;
            }
            else    //jump長押し終了
            {
                jumpRaising = false;
            }
        }

        //左に動くときは画像を反転
        if (moveValue.x > 0)
        {
            renderer.flipX = false;
        }
        else if (moveValue.x < 0)
        {
            renderer.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        if (!IsControllable)
            return;

        //横方向の速度計算
        Velocity = new Vector2(moveValue.x * moveSpeed, Velocity.y);

        //jump入力されてたら上に速度をつける
        if (jumpRequest)
        {
            Velocity = new Vector2(Velocity.x, jumpPower);
            jumpRequest = false;
        }

        //速度が下向きの時はheadcolliderを無効にする
        if (Velocity.y < 0f && headCollider.activeInHierarchy)
        {
            headCollider.SetActive(false);
        }

        //速度が上の時は有効にする
        if (Velocity.y >= 0f && !headCollider.activeInHierarchy)
        {
            headCollider.SetActive(true);
        }

        //落下速度に制限
        if (Velocity.y < -maxFallSpeed)
        {
            Velocity = new Vector2(Velocity.x, -maxFallSpeed);
        }
    }

    private bool OnGround()
    {
        //落下中なら
        if (Rb.linearVelocityY < -0.1f)
            return false;

        //足元に地面layerがあればtrue
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, jumpableLayer);
    }

    private bool CanContinueJump()
    {
        //ジャンプの長押し秒数が最大より小さい時true
        return jumpSecCnt < jumpHoldSecMax;
    }

    public void VertVelocityReset()
    {
        //jump上昇中を解除、鉛直方向の速度を0にする
        jumpRaising = false;
        Velocity = new Vector2(Velocity.x, 0f);
    }

    public void Warp(Vector3 target)
    {
        transform.position = target;
    }

    public void DisableControl()
    {
        IsControllable = false;
        MoveStop();     //動きも止める
    }

    public void RigitBodySimulateStart()
    {
        Rb.simulated = true;
    }

    public void EnableControl()
    {
        IsControllable = true;

        if (!Rb.simulated) RigitBodySimulateStart();    //演算が止まっていれば開始する
    }

    //頭がブロックなどに当たっているときジャンプ上昇をやめる
    public void OnHeadHitCeiling()
    {
        jumpRaising = false;
        //速度リセットはしない

        //ブロックが真上でなくても横とかでもヒットする　
        //今はpushblockに当たってもジャンプをやめないようにして無理やり解決してる状態
    }

    //キャラの動きを止める　死亡時用
    public void MoveStop()
    {
        Velocity = Vector2.zero;
        Rb.simulated = false;
    }

    //プレイヤーの登場アニメーションを再生し、終了を待つコルーチン
    public IEnumerator PlayerAppearAnimation()
    {
        //appearトリガーを起動してアニメーションを再生
        animator.SetTrigger("Appear");

        yield return null;//1フレーム待つ

        //appearアニメ―ションの終了を待つ
        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).IsName("Appear") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
    }

    //死亡アニメーション終了時
    public void OnDieAnimationEnd()
    {
        gameObject.SetActive(false);
        StageManager.Instance.OnPlayerDieAnimationEnd();
    }
}
