using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private int checkNumber;   //チェックポイントの番号

    [Header("ここから再開するときに無効化しておくギミック")]
    [SerializeField] private GameObject _object;    //ここから再開するときに無効化しておくギミック

    //static変数でチェックポイントの座標を登録しておくかもしれない

    private void Awake()
    {
        //無効化オブジェクトが登録されていて、そのチェックポイントから再開する場合のみ無効化
        if(checkNumber == GameManager.Instance?.CheckPoint && _object!=null)
        {
            _object.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //チェックポイントを更新したときだけメッセージを表示する
            if (GameManager.Instance.PassCheckPoint(checkNumber))
                StageManager.Instance.ShowMessage($"中間地点{checkNumber}通過");
        }
    }
}
