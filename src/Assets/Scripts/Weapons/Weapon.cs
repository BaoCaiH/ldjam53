using UnityEngine;

public interface Weapon
{
    void OnAttach(PlayerController player);
    void OnDetach(PlayerController player);
    void Apply(GameObject gameObject);
    void SetFacing(Vector2 playerFacing);
}