using UnityEngine;

class HeavyGlove : Weapon
{
    public Vector2 power = new Vector2(10, 2);

    private Vector2 currentFacing = new Vector2(1, 1);

    public void OnAttach(PlayerController player)
    {
        Debug.Log($"[HeavyGlove] OnAttach");
        player.rgbody.gravityScale = 15;
        player.rgbody.mass = 3;
        player.walkSpeed = PlayerController.DEFAULT_WALK_SPEED;
        player.runSpeed = PlayerController.DEFAULT_RUN_SPEED - 2;
    }

    public void OnDetach(PlayerController player)
    {
        Debug.Log($"[HeaveGlove] OnDetach");
        player.rgbody.gravityScale = 7;
        player.rgbody.mass = 1;
        player.walkSpeed = PlayerController.DEFAULT_WALK_SPEED;
        player.runSpeed = PlayerController.DEFAULT_RUN_SPEED;
    }

    public void Apply(GameObject gameObject)
    {
        Debug.Log($"[HeavyGlove] Apply attack to {gameObject.name}");

        BoxController boxController = gameObject.GetComponent<BoxController>();
        boxController.rgbody.velocity = power * currentFacing;
    }

    public void SetFacing(Vector2 playerFacing)
    {
        currentFacing = playerFacing;
    }
}