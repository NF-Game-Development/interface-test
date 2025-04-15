using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInteraction : MonoExt
{
    [SerializeField] private float _interactRange = 10f;
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private Camera _topDownCamera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Ray ray = _topDownCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * _interactRange, Color.green, 1f);

        if (Physics.Raycast(ray, out hit, _interactRange, _interactableLayer))
        {
            Debug.Log("Hit: " + hit.collider.name);
            
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
        else
        {
            Debug.Log("Nothing to interact with.");
        }
    }

    public void SetCamera(Camera camera)
    {
        _topDownCamera = camera;
    }
}
