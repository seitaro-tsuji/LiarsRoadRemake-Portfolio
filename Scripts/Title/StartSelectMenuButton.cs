using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartSelectMenuButton : MonoBehaviour
{
    [SerializeField] StartPointType startPointType;

    private void Awake()
    {
        GetComponent<Button>()?.onClick.AddListener(Clicked);
    }

    private void Clicked()
    {
        //押した場合、対応するstartPointからスタートする
        GameManager.Instance.SetStartPointType(startPointType);
        GameManager.Instance.StageEnter();
    }
}
