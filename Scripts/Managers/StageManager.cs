using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    [SerializeField] private int stage;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TMP_Text message;
    [SerializeField] private float messageDisplaySec = 5f;
    [SerializeField] private ResultPanel resultPanel;

    [Header("StartPoints")]
    [SerializeField] private List<Transform> startPoints;

    public static StageManager Instance { get; private set; } 

    private int coin;   //コインの取得数
    private int checkPointPass; //通過した最後のチェックポイント
    private string deathCause;  
    private string deathSpecialMessage; 

    private void Awake()
    {
        //シングルトン
        if (Instance != null && Instance != this)   //重複チェック　2個目以降は作らない
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        //初期設定
        coin = 0;
        checkPointPass = 0;
        deathCause = "";
        deathSpecialMessage = "";
    }

    private void Start()
    {
        //プレイヤーを開始地点に移動させる
        int startPoint = GameManager.Instance.StartInfo.startPoint;

        if (startPoints == null || startPoints.Count == 0)
        {
            Debug.LogWarning("startPointsが設定されていません。");
            return;
        }

        if(startPoint < 0 || startPoint >= startPoints.Count)
        {
            Debug.LogWarning($"{startPoint}番の開始地点がないため、0番からスタートします。");
            startPoint = 0;
        }

        playerController.transform.position = startPoints[startPoint].position;

        AudioManager.Instance.PlayStageBGM();
    }

    private void OnDestroy()
    {
        AudioManager.Instance.StopBGM();
    }

    public void AddCoin()
    {
        coin += 1;
        coinText.text = $"× {coin}";
    }

    //外部からメッセージ表示できるようにする　コルーチンをどのオブジェクトが管理するか分かりやすいように
    public void ShowMessage(string message)
    {
        StartCoroutine(MessageCoroutine(message));
    }

    //メッセージを表示する
    private IEnumerator MessageCoroutine(string str)
    {
        message.text = str;
        message.gameObject.SetActive(true); //メッセージの表示をオン

        yield return new WaitForSeconds(messageDisplaySec);

        message.gameObject.SetActive(false);    //メッセージを消す
    }

    //プレイヤーをワープさせる
    public IEnumerator PlayerWarp(Vector3 warpTarget)
    {
        playerController.DisableControl();

        //画面を暗転する
        yield return BlackPanel.Instance.FadeOut();

        //プレイヤーを移動
        playerController.Warp(warpTarget);  //ワープ
        playerController.gameObject.SetActive(false);   //一旦見えなくする

        //暗転解除
        yield return BlackPanel.Instance.FadeIn();

        //プレイヤーを再度アクティブに
        playerController.gameObject.SetActive(true);

        yield return playerController.PlayerAppearAnimation();   //アニメーション終了を待つ
        playerController.EnableControl();       //操作可能にする
    }

    //プレイヤー死亡時
    public void OnPlayerDieAnimationEnd()
    {
        StartCoroutine(PlayerDieSequence());
    }

    //死亡時コルーチン
    private IEnumerator PlayerDieSequence()
    {
        //画面を半分だけ暗転する
        yield return BlackPanel.Instance.FadeOut(0.75f);

        //リザルト画面を表示する
        resultPanel.Show(new DeathInfo(deathCause,deathSpecialMessage));
    }

    public void SetDeathCause(string str)
    {
        deathCause = "死因：" + str;
    }

    public void SetDeathCause()
    {
        deathCause = "";
    }

    public void SetDeathSpecialMessage(string str)
    {
        deathSpecialMessage = str;
    }

    public IEnumerator StageClearCoroutine()
    {
        //プレイヤーの動きを止めて操作不能にして暗転
        playerController.DisableControl();
        playerController.MoveStop();
        yield return BlackPanel.Instance.FadeOut(0.75f);
        
        //死因を消してクリア表示をする
        SetDeathCause();
        resultPanel.Show(clearStage: stage);
        GameManager.Instance.StageClear(stage);
    }
}
