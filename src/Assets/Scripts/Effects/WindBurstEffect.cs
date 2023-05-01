using UnityEngine;

class WindBurstEffect : Effect
{
    private Vector2 windPower;
    
    internal WindBurstEffect(Vector2 power)
    {
        windPower = power;
    }

    public void Apply(GameObject gameObject)
    {
        Debug.Log($"[WindBurstEffect] Apply wind burst effect with power {windPower}");

        Rigidbody2D rdbody = gameObject.GetComponent<Rigidbody2D>();
        rdbody.AddForce(windPower, ForceMode2D.Impulse);
    }
}