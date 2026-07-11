using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StartPointType
{
    Start=0,
    CheckPoint=1
}

//開始するステージと開始地点
public class StageEnterInfo
{
    public int stage;
    public int startPoint;  //スタート地点or中間地点1,2,3,...のどれからスタートするのか

    public StageEnterInfo(int stage, int point)
    {
        this.stage = stage;
        this.startPoint = point;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int LastPlayedStage {  get; private set; }   //最後にプレイしたステージ
    public int ClearStage { get; private set; }     //クリアしたステージ
    public int CheckPoint { get; private set; }     //到達したチェックポイント

    //プレイヤーがどこからスタートを選択したかを保存しておく
    public StageEnterInfo StartInfo {  get; private set; }


    private void Awake()
    {
        //シングルトン
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        ClearStage = 0;
        CheckPoint = 0;
        LastPlayedStage = 0;
        StartInfo = new StageEnterInfo(0,0);
    }

    public void SetStartPointType(StartPointType type)
    {
        //スタート地点開始ならスタート地点から、中間開始なら最後に到達したチェックポイントから
        if (type == StartPointType.Start)
            StartInfo.startPoint = 0;
        else 
            StartInfo.startPoint = CheckPoint;
    }

    public void SetEnterStage(int stage)
    {
        StartInfo.stage = stage;
    }

    public void StageEnter()
    {
        int stage = StartInfo.stage;    //どのステージか

        if (1 <= stage && stage <= 2)
        {
            SceneManager.LoadScene($"Stage{stage}Scene");
            LastPlayedStage = stage;
            Debug.Log($"Stage{stage}:{StartInfo.startPoint}番からスタート");
        }
        else if (stage == 3)
        {
            Debug.Log("ステージ3はまだありません。");
            return;
        }
    }
    
    //中間地点通過してたらそのスタート地点を返す　してなければfalse
    public bool TryGetStartPosition(out Vector3 position)
    {
        //中間地点通過済みの時
        if(CheckPoint > 0)
        {
            //if (startPos == null) Debug.LogError("GameManagerのstartPosが設定されていません。");
            //position = startPos;
            position = Vector3.zero;
            return true;
        }

        //通過してないとき
        position = Vector3.zero;
        return false;
    }

    //中間地点通過
    public bool PassCheckPoint(int _checkPoint)
    {
        //チェックポイント更新の時だけtrue
        if (_checkPoint > CheckPoint)
        {
            CheckPoint = _checkPoint;
            return true;
        }

        return false;
    }

    //中間地点通過情報をリセット
    public void ResetCheckPoint()
    {
        CheckPoint = 0;
    }

    //ステージクリア クリア情報を更新して中間情報をリセット
    public void StageClear(int stage)
    {
        if (stage > ClearStage)
            ClearStage = stage;
        ResetCheckPoint();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
