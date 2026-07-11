using System.Collections;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private float flipSec = 0.5f; //‰æ‘œ”½“]‚ÌŠÔŠÔŠu
    [SerializeField] private float frequency = 1f;       //ã‰º‚Ì“®‚«‚ÌU“®”
    [SerializeField] private float amplitude = 0.3f;    //“®‚«‚ÌU•

    private SpriteRenderer spriteRenderer;
    private float secCnt;       //•b
    private Vector3 center;     //U“®‚Ì’†S

    void Start()
    {
        secCnt = 0;
        center = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(FlipRoutine());
    }

    void Update()
    {
        secCnt += Time.deltaTime;

        //yÀ•W‚ğ’†S+sin2ƒÎft‚É‚·‚é
        float y = Mathf.Sin(2 * Mathf.PI * frequency * secCnt) * amplitude;
        transform.position = center + new Vector3(0, y, 0); 
    }

    //ˆê’èŠÔŠÔŠu‚Å‰æ‘œ”½“]
    private IEnumerator FlipRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(flipSec);

            spriteRenderer.flipY = !spriteRenderer.flipY;
        }
    }
}
