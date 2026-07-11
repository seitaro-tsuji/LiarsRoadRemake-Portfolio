using UnityEngine;

public class HeadCollider : MonoBehaviour
{
    [SerializeField] private LayerMask itemBlockLayer;
    [SerializeField] private PlayerController _controller;
    [SerializeField] private LayerMask dontRaiseStopLayer;  //ぶつかってもジャンプ中の上昇を止めないlayer　coinなど

    //ブロックを叩く処理
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log($"Enter: {other.name}, velocityY = {_controller.Rb.linearVelocity.y}");

        //天井にあたった時、速度関係なく長押しジャンプを止める
        if (!GameUtils.IsInLayerMask(other, dontRaiseStopLayer))
        {
            _controller.OnHeadHitCeiling();
            AudioManager.Instance.PlaySE("bump");   //頭をぶつけるSEを鳴らす
        }

        //プレイヤーが上昇中以外は叩かない
        if (_controller.Rb.linearVelocity.y <= 0f)
           return;

        //叩いたものがアイテムブロックなら
        if (GameUtils.IsInLayerMask(other, itemBlockLayer))
        {
            ItemBlock itemBlock = other.GetComponent<ItemBlock>();
            if (itemBlock != null)
            {
                itemBlock.Hit();
            }
        }

        //頭がぶつかったら縦の速度をリセットする
        if(!GameUtils.IsInLayerMask(other, dontRaiseStopLayer))
        {
            _controller.VertVelocityReset();
        }
    }
}
