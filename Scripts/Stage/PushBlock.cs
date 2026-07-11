using UnityEngine;

//プレイヤーを押し出すように動くブロック　数マスだけ動いて止まる
public class PushBlock : MonoBehaviour,ITrap
{
    [SerializeField] private Vector2 moveDirection; //動く方向(使うときは規格化するので向きだけ合えばいい
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveDistance;

    [SerializeField] private bool canRepeat;

    private Vector2 normalizedDirection;
    private float restDistance;
    private bool isActive;

    private void Awake()
    {
        restDistance = moveDistance;
        isActive = false;
        normalizedDirection = moveDirection.normalized;
    }

    public void ActivateTrap()
    {
        if (isActive) return;

        if (canRepeat)
            restDistance = moveDistance;

        isActive = true;
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            //動くベクトルを計算
            Vector2 moveVector = normalizedDirection * moveSpeed * Time.fixedDeltaTime;

            //動く距離を計算して、moveDistanceを超えるならmovedistanceまでで止まる
            if (moveVector.magnitude > restDistance) 
            {
                moveVector = normalizedDirection * restDistance;
            }

            //移動して残り距離の更新
            transform.position += (Vector3)moveVector;
            restDistance -= moveVector.magnitude;

            //移動終了したらactiveでなくなる
            if(restDistance < 0.01f)
                isActive = false;
        }
    }
}
