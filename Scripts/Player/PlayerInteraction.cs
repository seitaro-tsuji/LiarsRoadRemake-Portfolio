using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(PlayerStatus))]
public class PlayerInteraction : MonoBehaviour
{
    private LayerMask coinLayer;
    private LayerMask hazardLayer;
    private PlayerStatus status;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinLayer = GameUtils.GetLayerMask("Coin");
        hazardLayer = GameUtils.GetLayerMask("Hazard");
        status = GetComponent<PlayerStatus>();
    }

    //‘¼‚Ì‚à‚Ì(trigger)‚É“–‚½‚Á‚½‚Æ‚«‚Ìˆ—
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //ƒRƒCƒ“‚ÉG‚ê‚½
        if(GameUtils.IsInLayerMask(collision, coinLayer))
        {
            Destroy(collision.gameObject); //ƒRƒCƒ“‚ğÁ‚·
            StageManager.Instance.AddCoin();
            AudioManager.Instance.PlayCoinSE(); //SE
        }

        //UŒ‚”»’è‚ÉG‚ê‚½
        if (GameUtils.IsInLayerMask(collision, hazardLayer))
        {
            //—‰º”»’è‚Å€‚ñ‚¾‚Æ‚«
            if (collision.CompareTag("FallDeathZone"))
            {
                status.OnDie("—‰º€");
            }
            //‹UƒS[ƒ‹‚Ì
            else if (collision.CompareTag("FakeGoal"))
            {
                status.OnDie("????");
            }
            else//—‰ºˆÈŠO‚Ì€ˆö‚ÍƒgƒQ‚µ‚©‚È‚¢(¡‚Ì‚Æ‚±‚ë)
            {
                status.OnDie("ƒgƒQ");
            }
        }

        //Œ®‚ÉG‚ê‚½
        if (collision.CompareTag("Key"))
        {
            Destroy(collision.gameObject);  //Œ®‚ğÁ‚·
            Debug.Log("–¢À‘•:ƒJƒM‚ğæ“¾");

            //GameManager‚ÉƒJƒM‚Ìæ“¾‚ğ’Ê’m‚·‚é
        }
    }
}
