using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float fixedY = 0f; //カメラのy座標
    [SerializeField] private GameObject cameraLeftWall; //カメラの左端の壁


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ステージ1開始時は左に行けるようにする　念のためにここでも処理を書いている
        if (cameraLeftWall != null && GameManager.Instance.StartInfo.stage == 1)
            cameraLeftWall.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーがカメラ位置よりも右にあればカメラを右移動する　左にはカメラは移動しない
        if (player.position.x > transform.position.x)
        {
            transform.position = new Vector3(player.transform.position.x, fixedY, transform.position.z);    //カメラ移動

            //cameraLeftWallが無効化されているときは有効にする
            if (cameraLeftWall != null && !cameraLeftWall.activeInHierarchy)
            {
                cameraLeftWall.SetActive (true);
            }
        }
    }
}
