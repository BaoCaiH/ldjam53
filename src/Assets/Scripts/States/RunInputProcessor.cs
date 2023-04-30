class RunInputProcessor : PlayerInputProcessor 
{
    public void Enter(PlayerController player)
    {
        player.currentSpeed = 8f;
        player.animator.SetBool(AnimationParams.RUN_FLAG, true);
    }

    public bool Process(PlayerController player)
    {
        return true;
    }

    public void Exit(PlayerController player)
    {
        player.currentSpeed = 4f;
        player.animator.SetBool(AnimationParams.RUN_FLAG, false);
    }
}