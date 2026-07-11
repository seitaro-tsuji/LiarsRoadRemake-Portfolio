using UnityEngine;

public class FloatMotion : MonoBehaviour
{
    [SerializeField] private float flipSec = 0.5f; //‰æ‘œ”½“]‚ÌŠÔŠÔŠu
    [SerializeField] private float frequency = 1f;       //ã‰º‚Ì“®‚«‚ÌU“®”
    [SerializeField] private float amplitude = 0.3f;    //“®‚«‚ÌU•

    private float secCnt;       //•b
    private Vector3 center;     //U“®‚Ì’†S

    void Start()
    {
        secCnt = 0;
        center = transform.localPosition;
    }

    void Update()
    {
        secCnt += Time.deltaTime;

        //yÀ•W‚ğ’†S+sin2ƒÎft‚É‚·‚é
        float y = Mathf.Sin(2 * Mathf.PI * frequency * secCnt) * amplitude;
        transform.localPosition = center + new Vector3(0, y, 0);
    }
}
