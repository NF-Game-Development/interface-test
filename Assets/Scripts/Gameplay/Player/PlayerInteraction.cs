using UnityEngine;

public class PlayerInteraction : MonoExt
{
    public float interactRange = 10f;
    public LayerMask interactableLayer;
    public Camera topDownCamera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Ray ray = topDownCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * interactRange, Color.green, 1f);

        if (Physics.Raycast(ray, out hit, interactRange, interactableLayer))
        {
            Debug.Log("Hit: " + hit.collider.name);
        }
        else
        {
            Debug.Log("Nothing to interact with.");
        }
    }
}
