using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlackPanel : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = 1f;    //暗転、暗転解除のスピード

    public static BlackPanel Instance { get; private set; } //シングルトン

    private Image image;
    public float BlackAlpha //透明度プロパティ
    {
        get
        {
            return image.color.a;
        }
        set
        {
            Color c = image.color;
            c.a = Mathf.Clamp01(value);
            image.color = c;
        }
    }

    private Coroutine fadeCoroutine;

    private void Awake()
    {
        Instance = this;
        image = GetComponent<Image>();
    }

    //画面を暗転させる
    public IEnumerator FadeOut(float targetAlpha=1f)
    {
        yield return StartCoroutine(FadeCoroutine(targetAlpha));
    }

    //暗転解除
    public IEnumerator FadeIn(float targetAlpha = 0f)
    {
        yield return StartCoroutine(FadeCoroutine(targetAlpha));
    }

    //暗転or暗転解除
    private IEnumerator FadeCoroutine(float targetAlpha)
    {
        //目標値まで透明度を変化させる
        while(BlackAlpha != targetAlpha)
        {
            BlackAlpha = Mathf.MoveTowards(BlackAlpha, targetAlpha, fadeSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
