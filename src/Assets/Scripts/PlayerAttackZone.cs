using UnityEngine;

public class PlayerAttackZone : MonoBehaviour
{
    private void Awake()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            PlayerController playerController = GetComponentInParent<PlayerController>();
            BoxController boxController = collision.gameObject.GetComponent<BoxController>();

            boxController.ReceiveAttack(playerController.currentWeapon);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}
