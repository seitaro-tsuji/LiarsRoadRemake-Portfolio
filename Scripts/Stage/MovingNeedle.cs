using UnityEngine;
using UnityEngine.UIElements;

public class MovingNeedle : MonoBehaviour,ITrap
{
    [SerializeField] private Vector2 moveSpeedVector = new Vector2(-20, 0); //動く速度ベクトル

    private bool isActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isActive = false;   //最初は発動していない状態にする

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //発動中なら移動する
        if (isActive)
        {
            Vector3 moveSpeedVector3 = moveSpeedVector; //vector3に変換
            transform.position += moveSpeedVector3 * Time.deltaTime;
        }
    }

    public void ActivateTrap()
    {
        isActive = true;
    }
}
