using UnityEngine;

public class InvisibleBlockTrigger : ItemBlock
{
    [SerializeField] private GameObject block;  //ブロック本体

    private LayerMask playerHeadLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHeadLayer = GameUtils.GetLayerMask("PlayerHead");

        //ブロックは最初は無効にする
        block.SetActive(false);
    }

    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーが叩いた時、ブロックを有効化する
        if (GameUtils.IsInLayerMask(collision, playerHeadLayer))
        {
            block.SetActive(true);
        }
    }
}
