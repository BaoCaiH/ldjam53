using UnityEngine;
using UnityEngine.SceneManagement;

class ResetGlove : Weapon
{
    private Vector2 currentFacing = new Vector2(1, 1);

    public void OnAttach(PlayerController player)
    {

    }

    public void OnDetach(PlayerController player)
    {

    }

    public void Apply(GameObject gameObject)
    {
        Debug.Log($"[ResetGlove] Apply attack to {gameObject.name}");

        TruckController[] objs = GameObject.FindObjectsOfType(typeof(TruckController)) as TruckController[];
        Debug.Log($"[ResetGlove] Found TruckController: {objs.Length}");
        
        if (objs.Length > 0)
        {
            Debug.Log($"[ResetGlove] Resetting current scene...");

            TruckController truck = objs[0];
            
            truck.GetComponent<SceneLoader>().sceneName = SceneManager.GetActiveScene().name;
            truck.PostLeaving();
        }
    }

    public void SetFacing(Vector2 playerFacing)
    {
        currentFacing = playerFacing;
    }
}