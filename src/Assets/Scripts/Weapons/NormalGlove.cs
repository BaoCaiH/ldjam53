using UnityEngine;

class NormalGlove : Weapon
{
    public Vector2 power = new Vector2(7, 2);

    private Vector2 currentFacing = new Vector2(1, 1);

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