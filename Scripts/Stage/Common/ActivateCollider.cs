using System.Collections.Generic;
using UnityEngine;

public class ActivateCollider : MonoBehaviour
{
    [SerializeField] private List<GameObject> activateTargets;  //activateする対称のオブジェクト

    private LayerMask playerLayer;
                                                         
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerLayer = GameUtils.GetLayerMask("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //トリガーに入ったのがプレイヤーならトラップが発動する
        if (GameUtils.IsInLayerMask(collision, playerLayer))
        {
            foreach (GameObject target in activateTargets)
            {

                ITrap trap = target.GetComponent<ITrap>();

                if (trap != null)
                {
                    trap.ActivateTrap();
                }
            }
        }
    }
}
