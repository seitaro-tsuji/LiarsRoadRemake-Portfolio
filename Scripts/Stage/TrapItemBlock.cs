using UnityEngine;

public class TrapItemBlock : ItemBlock,ITrap
{
    [SerializeField] private GameObject needle; //とげの表示
    [SerializeField] private PlayerController playerController;

    void Start()
    {
        needle.SetActive(false);  //最初はとげは非表示
    }

    public override bool Hit()
    {
        ActivateTrap();//発動
        return true;
    }

    public void ActivateTrap()
    {
        //死亡判定
        //Debug.Log("未実装:プレイヤー死亡処理");

        needle.SetActive(true); //とげを出す
        //playerController.DisableControl();  //操作不可能にする
    }
}
