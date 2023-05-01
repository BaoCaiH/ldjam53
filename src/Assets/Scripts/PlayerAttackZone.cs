using UnityEngine;

public class PlayerAttackZone : MonoBehaviour
{
    [SerializeField] private Sprite palmNormal;
    [SerializeField] private Sprite backNormal;
    [SerializeField] private Sprite palmSky;
    [SerializeField] private Sprite backSky;
    [SerializeField] private Sprite palmBackward;
    [SerializeField] private Sprite backBackward;
    [SerializeField] private Sprite palmHeavy;
    [SerializeField] private Sprite backHeavy;
    [SerializeField] private Sprite palmRestart;
    [SerializeField] private Sprite backRestart;
    [SerializeField] private GameObject backhand;

    private SpriteRenderer primarySprite;
    private SpriteRenderer secondarySprite;

    private void Awake()
    {
        primarySprite = GetComponent<SpriteRenderer>();
        secondarySprite = backhand.GetComponent<SpriteRenderer>();
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

    public void SetSprite(string name)
    {
        switch (name) {
            case "normal":
                primarySprite.sprite = palmNormal;
                secondarySprite.sprite = backNormal;
                break;
            case "sky":
                primarySprite.sprite = palmSky;
                secondarySprite.sprite = backSky;
                break;
            case "backward":
                primarySprite.sprite = palmBackward;
                secondarySprite.sprite = backBackward;
                break;
            case "heavy":
                primarySprite.sprite = palmHeavy;
                secondarySprite.sprite = backHeavy;
                break;
            case "reset":
                primarySprite.sprite = palmRestart;
                secondarySprite.sprite = backRestart;
                break;
            default:
                primarySprite.sprite = palmNormal;
                secondarySprite.sprite = backNormal;
                break;
        }
    }
}
