using UnityEngine;

public class AccelerateTrigger : MonoBehaviour
{
    [Header("‰Á‘¬‚µ‚½Œã‚Ì‘¬‚³")]
    [SerializeField] private float acceleratedSpeed;        //‰Á‘¬Œã‚Ì‘¬‚³

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //‚Ô‚Â‚©‚Á‚½•¨‘Ì‚ªMovingObject‚ğ‚Á‚Ä‚¢‚½‚ç
        if (collision.TryGetComponent(out MovingObject movingObject))
        {
            movingObject.Accelerate(acceleratedSpeed);
        }

        return;
    }
}
