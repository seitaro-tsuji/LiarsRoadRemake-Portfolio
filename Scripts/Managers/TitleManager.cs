using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager: MonoBehaviour
{
    [SerializeField] private List<Button> stageButtons;

    [Header("デバッグ用")]
    [SerializeField] private bool allButtonsEnabled;

    public TitleManager Instance { get; set; }
    
    private void Awake()
    {
        //シングルトン
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (allButtonsEnabled)
            return;

        //未クリアのステージのボタンを無効にする
        for (int i = GameManager.Instance.ClearStage+1; i < stageButtons.Count; i++)
        {
            stageButtons[i].interactable = false;
        }
    }

}
