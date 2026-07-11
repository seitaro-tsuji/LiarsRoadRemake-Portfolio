using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource seSource;

    [Header("BGM Clips")]
    [SerializeField] private AudioClip stageBGM;

    [Header("SE Clips")]
    [SerializeField] private AudioClip jumpSE;
    [SerializeField] private AudioClip coinSE;
    [SerializeField] private AudioClip bumpSE;
    [SerializeField] private AudioClip dieSE;
    [SerializeField] private AudioClip selectSE;

    [Header("SE Volumes")]
    [SerializeField, Range(0f, 1f)]
    private float coinVolume = 0.3f;

    private void Awake()
    {
        //シングルトンの処理
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //PlayBGM(stageBGM);//ステージBGMを再生
    }

    //ループしてBGMを流す
    private void PlayBGM(AudioClip clip)
    {
        if (clip == null) return;

        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    //1回だけSEを流す
    void PlaySE(AudioClip clip)
    {
        if (clip == null) return;

        seSource.PlayOneShot(clip);
    }

    public void PlaySE(string seKind, float volume = 1.0f)
    {
        //stringからクリップに変換
        AudioClip clip;

        if (seKind == "jump") clip = jumpSE;
        else if (seKind == "coin")  clip = coinSE;
        else if (seKind == "bump") clip = bumpSE;
        else if (seKind == "die") clip = dieSE;
        else if (seKind == "select") clip = selectSE;
        else
        {
            Debug.LogError($"SEクリップが見つかりません:{seKind}");
            return;
        }

        //再生
        seSource.PlayOneShot(clip, volume);
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlayJumpSE() => PlaySE(jumpSE);
    public void PlayCoinSE() => PlaySE("coin", coinVolume);

    public void PlaySelectSE() => PlaySE(selectSE);

    public void PlayStageBGM() => PlayBGM(stageBGM);
}
