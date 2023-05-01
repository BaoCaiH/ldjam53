using UnityEngine;

public class WindBurstController : MonoBehaviour
{
    public Vector2 windPower = new Vector2(7.3f, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log($"[WindBurstController] Player entered burst zone!");

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.OnEffectAdd(new WindBurstEffect(windPower));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log($"[WindBurstController] Player left burst zone!");

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.OnEffectRemove<WindBurstEffect>();
        }
    }
}
