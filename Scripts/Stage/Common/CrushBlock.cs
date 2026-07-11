using UnityEngine;

public class CrushBlock : MonoBehaviour
{
    [SerializeField] private LayerMask crusherLayer;        //groundなど圧死の対象にできるブロック
    [SerializeField] private float checkDistance = 0.08f;   //チェックする点の距離

    //このmoveDirectionはfallblockやraiseblockなどから設定してもらう
    private Vector2 moveDirection;
    public Vector2 MoveDirection { set { moveDirection = value.normalized; } }

    private LayerMask playerLayer;

    private void Awake()
    {

        playerLayer = GameUtils.GetLayerMask("Player");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //接触しているのがプレイヤー以外なら
        if (!GameUtils.IsInLayerMask(collision.collider, playerLayer))
            return;

        //動いていない場合は何もしない
        if (moveDirection == Vector2.zero)
            return;

        //プレイヤーを少し動かした先が壁や床などなら圧死
        var playerCollider = collision.collider;
        if(CheckWallInDirection(playerCollider, moveDirection))
        {
            var playerStatus = playerCollider.GetComponent<PlayerStatus>();
            if (playerStatus != null)
            {
                playerStatus.OnDie("圧死");
            }
        }
    }

    private bool CheckWallInDirection(Collider2D playerCollider, Vector2 direction)
    {
        //動いてなければ終了
        if(direction == Vector2.zero)
            return false;

        ContactFilter2D filter = new ContactFilter2D();
        filter.useLayerMask = true;
        filter.layerMask = crusherLayer;
        filter.useTriggers = false;

        RaycastHit2D[] results = new RaycastHit2D[4];

        //プレイヤーをずらした先に壁やブロックがあるかどうかの判定
        int count = playerCollider.Cast(direction, filter, results, checkDistance);

        //自分自身や子オブジェクト、さらに進行方向と関係ない接し方のブロックは無視する
        for (int i = 0; i < count; i++)
        {
            if (results[i].collider == null)
                continue;

            if (results[i].collider.transform.IsChildOf(transform))
                continue;

            float dot = Vector2.Dot(results[i].normal, -direction);
            if(dot < 0.7f)
                continue;

            return true;
        }

        return false;
    }
}
