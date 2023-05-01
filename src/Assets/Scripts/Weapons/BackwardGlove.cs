using UnityEngine;

class BackwardGlove : Weapon
{
    public Vector2 power = new Vector2(7, 2);

    private Vector2 flipper = new(-1f, 1f);

    private Vector2 currentFacing = new Vector2(-1, 1);

    public void OnAttach(PlayerController player)
    {

    }

    public void OnDetach(PlayerController player)
    {

    }

    public void Apply(GameObject gameObject)
    {
        Debug.Log($"[BackwardGlove] Apply attack to {gameObject.name}");
        Debug.Log($"[BackwardGlove] Current weapon facing: {currentFacing}");

        BoxController boxController = gameObject.GetComponent<BoxController>();
        boxController.rgbody.velocity = power * currentFacing;
    }

    public void SetFacing(Vector2 playerFacing)
    {
        Debug.Log($"[BackwardGlove] Update glove facing {currentFacing} from player facing {playerFacing}");
        currentFacing = playerFacing * flipper;
        Debug.Log($"[BackwardGlove] Updated glove facing to {currentFacing}");
    }
}