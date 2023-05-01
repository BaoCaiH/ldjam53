using UnityEngine;

class WindBurstEffect : Effect
{
    private Vector2 windPower;

    private float timePassed;

    internal WindBurstEffect(Vector2 power)
    {
        windPower = power;
        timePassed = 0f;
    }

    public void Apply(GameObject gameObject)
    {
        Debug.Log($"[WindBurstEffect] Apply wind burst effect with power {windPower}");

        Rigidbody2D rdbody = gameObject.GetComponent<Rigidbody2D>();
        rdbody.AddForce(windPower, ForceMode2D.Impulse);

        if (gameObject.tag == "Player")
        {
            PlayerController player = gameObject.GetComponent<PlayerController>();
            Debug.Log($"[WindBurstEffect] Player is wearing {player.currentWeapon.GetType()}");
            if (player.currentWeapon.GetType() != typeof(HeavyGlove))
            {
                Vector2 superWindPower = new Vector2(windPower.x * 10f, windPower.y);

                Debug.Log($"[WindBurstEffect] Not wearing HeavyGlove. Prepare to burst {superWindPower} in {Time.time - timePassed}/2 seconds :)");
                
                if (timePassed == 0)
                {
                    timePassed = Time.time;
                }
                else if (Time.time - timePassed >= 2)
                {
                    Debug.Log($"[WindBurstEffect] Flying time :)");

                    rdbody.AddForce(superWindPower, ForceMode2D.Impulse);
                    timePassed = 0;
                }
            }
        }
    }
}