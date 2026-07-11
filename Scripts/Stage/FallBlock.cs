using System.Collections;
using UnityEngine;

public class FallBlock : MonoBehaviour,ITrap
{
    [SerializeField] private float fallSpeed = 20f;
    [SerializeField] private float destroyDelaySec = 1f;
    [SerializeField] private float activateDelaySec = 0f;

    private Vector3 fallVector;
    private bool isActive; 

    private CrushBlock crushBlock;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isActive = false;
        fallVector = new Vector3(0, -fallSpeed, 0);
        crushBlock = GetComponent<CrushBlock>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //activeなら落下する
        if (isActive)
        {
            transform.position += fallVector * Time.deltaTime; 
        }

        //crushBlockがある場合は動く向きを設定する
        if(crushBlock != null)
        {
            crushBlock.MoveDirection = Vector2.down;
        }
    }

    public void ActivateTrap()
    {
        StartCoroutine(ActivateAndDestroy());
    }

    //時間差で発動して、時間差でオブジェクトを消す
    private IEnumerator ActivateAndDestroy()
    {
        yield return new WaitForSeconds(activateDelaySec);

        isActive = true;

        yield return new WaitForSeconds(destroyDelaySec);

        Destroy(gameObject);
    }
}
