using UnityEngine.InputSystem;

interface PlayerState
{
    void OnEnter(PlayerController player);
    PlayerState OnMove(InputAction.CallbackContext context, PlayerController player);
    PlayerState OnAttack(InputAction.CallbackContext context, PlayerController player);
}
