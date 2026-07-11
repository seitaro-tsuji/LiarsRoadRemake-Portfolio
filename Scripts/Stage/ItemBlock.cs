using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class ItemBlock : MonoBehaviour
{
    [SerializeField] private Sprite empty;  //叩いた後の画像

    private bool isHitable;   //叩く前かあとか

    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        isHitable = true; //叩けるように
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //叩いた時の処理
    public virtual bool Hit()
    {
        if (isHitable)
        {
            spriteRenderer.sprite = empty;
            isHitable = false;//叩けないように

            //コインやアイテムの処理
            Debug.Log("未実装:コインやアイテムを出す");

            return true;
        }
        return false;
    }
}
