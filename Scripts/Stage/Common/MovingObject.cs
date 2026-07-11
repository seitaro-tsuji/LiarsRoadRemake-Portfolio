using UnityEngine;
using System.Collections.Generic;
using System;

//コライダーと動く方向のセット
[Serializable]
public class MoveTrigger
{
    public MovingObjectTrigger trigger;
    public Vector2 direction;
    public float speed; 
}

public class MovingObject : MonoBehaviour
{
    [SerializeField] private List<MoveTrigger> moveDirections;
    [SerializeField] private bool useAcceleration;

    private bool isActive;        //trueなら動く
    private Vector2 moveDirection;
    private float speed;

    private CrushBlock crushBlock;

    private void Awake()
    {
        isActive = false;
        crushBlock = GetComponent<CrushBlock>();
    }
    
    //トリガーのどれかにプレイヤーが触れた時に呼ばれる　※コライダーにもスクリプトが必要なので注意!!
    public void Activate(Collider2D touchedTrigger)
    {
        //触れられたトリガーを取得
        MoveTrigger moveTrigger = 
            moveDirections.Find(x=>x.trigger == touchedTrigger.GetComponent<MovingObjectTrigger>());

        if (moveTrigger == null)
        {
            Debug.LogWarning($"未登録のトリガーです。{touchedTrigger.name}");
            return;
        }

        //非アクティブの場合アクティブにする
        if (!isActive)
        {
            moveDirection = moveTrigger.direction.normalized;
            speed = moveTrigger.speed;
            isActive = true;
        }
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            transform.position += (Vector3)moveDirection * speed * Time.fixedDeltaTime;
        }

        //CrushBlockがある場合は動く向きを設定する
        if(crushBlock != null)
        {
            crushBlock.MoveDirection = moveDirection;
        }
    }

    //何かの要因で加速する
    public void Accelerate(float acceleratedSpeed)
    {
        speed = acceleratedSpeed;
    }
}
