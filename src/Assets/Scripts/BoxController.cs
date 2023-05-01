using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private Sprite damagedSprite;
    [SerializeField] private int rigidIndex = 2;

    internal Rigidbody2D rgbody;
    private Vector2 incomingForce;
    private List<Weapon> incomingAttacks;

    private void Awake()
    {
        rgbody = GetComponent<Rigidbody2D>();
        incomingAttacks = new List<Weapon>();
    }

    private void FixedUpdate()
    {
        ApplyAttack();
    }

    private void ApplyAttack()
    {
        incomingAttacks.ForEach((attack) => attack.Apply(gameObject));
        incomingAttacks.Clear();
    }

    public void ReceiveAttack(Weapon weapon)
    {
        Debug.Log($"[BoxController] Receive attack from {weapon.GetType()}");
        incomingAttacks.Add(weapon);
    }

    public void Damage()
    {
        if (rigidIndex > 0)
        {
            rigidIndex--;
        }
        if (rigidIndex == 1)
        {
            GetComponent<SpriteRenderer>().sprite = damagedSprite;
        }
    }

    public bool IsBroken()
    {
        return rigidIndex == 0;
    }
}
