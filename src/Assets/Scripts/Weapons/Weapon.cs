using UnityEngine;

public interface Weapon
{
    void Apply(GameObject gameObject);
    void SetFacing(Vector2 facing);
}