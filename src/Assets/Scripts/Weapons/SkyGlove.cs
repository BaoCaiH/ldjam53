using UnityEngine;

class SkyGlove : Weapon
{
    public Vector2 power = new Vector2(2, 7);

    private Vector2 currentFacing = new Vector2(1, 1);

    public void OnAttach(PlayerController player)
    {

    }

    public void OnDetach(PlayerController player)
    {

    }

    public void Apply(GameObject gameObject)
    {
        Debug.Log($"[NormalGlove] Apply attack to {gameObject.name}");

        BoxController boxController = gameObject.GetComponent<BoxController>();
        boxController.rgbody.velocity = power * currentFacing;
    }

    public void SetFacing(Vector2 playerFacing)
    {
        currentFacing = playerFacing;
    }
}