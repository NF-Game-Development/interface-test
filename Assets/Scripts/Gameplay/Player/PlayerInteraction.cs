using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInteraction : MonoExt
{
    [SerializeField] private float _interactRange;
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private Camera _topDownCamera;
    [SerializeField] private float _collectRadius;
    [SerializeField] private SphereCollider _collider;
    
    [SerializeField] private List<ItemDropExtendableEnum> _collectedObjects = new List<ItemDropExtendableEnum>();

    private void Start()
    {
        _collider.radius = _collectRadius;
    }

    private void Update()
    {
        //for testing only
        _collider.radius = _collectRadius;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Interact();
        }
    }

    private void Interact()
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

    private void OnTriggerEnter(Collider other)
    {
        Collectable collectable = other.GetComponent<Collectable>();
        if (collectable != null)
        {
            collectable.SetPlayerInteraction(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Collectable collectable = other.GetComponent<Collectable>();
        if (collectable != null)
        {
            collectable.SetPlayerInteraction(null);
        }
    }

    public void SetCamera(Camera camera)
    {
        _topDownCamera = camera;
    }

    public void AddCollectable(ItemDropExtendableEnum _itemDrop)
    {
        _collectedObjects.Add(_itemDrop);
    }
}
