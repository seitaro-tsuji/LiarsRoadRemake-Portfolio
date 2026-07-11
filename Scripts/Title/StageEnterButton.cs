using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class StageEnterButton : MonoBehaviour
{
    [SerializeField] private int stage;
    [SerializeField] private GameObject startSelectMenu;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(StageEnter);
    }

    void StageEnter()
    {
        //始めるステージを登録する
        GameManager.Instance.SetEnterStage(stage);

        //このステージが最後にプレイしたステージで、かつ中間地点到達済みなら
        if (GameManager.Instance.LastPlayedStage == stage && GameManager.Instance.CheckPoint != 0)
        {
            startSelectMenu.SetActive(true); 
            return;
        }
        else if (GameManager.Instance.LastPlayedStage != stage)
        {
            GameManager.Instance.ResetCheckPoint();
            //returnせずに以下の処理に続く
        }

        //中間未到達なら直接ステージに入る
        GameManager.Instance.SetStartPointType(StartPointType.Start);
        GameManager.Instance.StageEnter();     //ステージに入る処理はGameManagerが行う
    }
}
