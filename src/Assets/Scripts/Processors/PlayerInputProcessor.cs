interface PlayerInputProcessor
{
    void Enter(PlayerController player);
    bool Process(PlayerController player);
    void Exit(PlayerController player);
}
