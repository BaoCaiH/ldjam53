using UnityEngine.InputSystem;

interface PlayerState
{
    void OnEnter(PlayerController player);
    PlayerState OnMove(InputAction.CallbackContext context, PlayerController player);
    PlayerState OnRun(InputAction.CallbackContext context, PlayerController player);
    PlayerState OnAttack(InputAction.CallbackContext context, PlayerController player);
    PlayerState OnJump(InputAction.CallbackContext context, PlayerController player);
    PlayerState OnUpdate(PlayerController player);
    void OnExit(PlayerController player);
}
