using UnityEngine;

public class FakeMovingNeedle : MonoBehaviour
{
    [SerializeField] private Vector2 direction1;
    [SerializeField] private float speed1;
    [SerializeField] private Vector2 direction2;
    [SerializeField] private float speed2;

    private Vector2 direction;
    private float speed;
    private bool isActive;

    private void Awake()
    {
        direction = Vector2.zero;
        speed = 0f;
        isActive = false;
    }

    private void FixedUpdate()
    {
        if (!isActive)
            return;

        //移動する
        transform.position += (Vector3)direction * speed * Time.fixedDeltaTime;
    }

    //動き出す　triggerNumberはトリガーの番号  1を呼んだあとでも2で上書き可能
    public void Activate(int triggerNumber)
    {
        if (triggerNumber == 1)
        {
            direction = direction1.normalized;
            speed = speed1;
        }
        else if (triggerNumber == 2)
        {
            direction = direction2.normalized;
            speed = speed2;
        }
        else
        {
            Debug.LogWarning($"triggerNumberが不正です。name:{gameObject.name}, triggerNumber{triggerNumber}");
            return;
        }

        isActive = true;
    }
}
