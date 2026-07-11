using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GoToTitleButton : MonoBehaviour
{
    [SerializeField] private bool isUI;
    [SerializeField] private PlayerStatus playerStatus;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(GoToTitleScene);

        //UI‚Ì•û‚Ìƒ{ƒ^ƒ“‚Í€–S‚ÉÁ‚·
        if (isUI)
            playerStatus?.OnPlayerDead.AddListener(HideButton);
    }

    private void OnDestroy()
    {
        playerStatus?.OnPlayerDead.RemoveListener(HideButton);
    }

    private void GoToTitleScene()
    {
        GameManager.Instance.LoadScene("TitleScene");
    }
    private void HideButton()
    {
        gameObject.SetActive(false);
    }
}
