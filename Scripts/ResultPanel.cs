using TMPro;
using UnityEngine;
using UnityEngine.UI;

//死亡情報のクラス　情報が増えてきたら別ファイルに分ける
public class DeathInfo
{
    public string cause;
    public string specialMessage;
    //ここにstaticとかで死亡回数をカウントする可能性あり

    public DeathInfo(string cause, string specialMessage="")
    {
        this.cause = cause;
        this.specialMessage = specialMessage;
    }
}

public class ResultPanel : MonoBehaviour
{
    [Header("Result Text")]
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private Color deathColor = Color.red;
    [SerializeField] private Color clearColor = Color.yellow;

    [Header("Other")]
    [SerializeField] private TMP_Text deathCauseText;
    [SerializeField] private TMP_Text specialMessageText;
    [SerializeField] private Button titleButton;

    private void Awake()
    {
        //タイトルボタンを押したときの挙動
        titleButton.onClick.RemoveAllListeners();
        titleButton.onClick.AddListener(GoTitleSceneButton);
    }

    private void Start()
    {
        gameObject.SetActive(false);    // Awake後に非表示
    }

    //死亡時のリザルト
    public void Show(DeathInfo deathInfo)
    {
        gameObject.SetActive(true);

        //各種メッセージを残す
        resultText.text = "死んでしまった…。";
        resultText.color = deathColor;
        deathCauseText.text = deathInfo.cause;
        specialMessageText.text = deathInfo.specialMessage;
    }

    //クリア時のリザルト
    public void Show(int clearStage)
    {
        gameObject.SetActive(true);

        //各種メッセージ
        resultText.text = $"ステージ{clearStage} クリア!!";
        resultText.color = clearColor;
        deathCauseText.text = "";
        specialMessageText.text = "";
    }

    private void GoTitleSceneButton()
    {
        AudioManager.Instance.PlaySelectSE();
        GameManager.Instance.LoadScene("TitleScene");
    }
}
