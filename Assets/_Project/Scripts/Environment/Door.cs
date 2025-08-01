using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private bool _isOpen = false;
    
    public void Interact()
    {
        if (!_isOpen)
        {
            Debug.Log("Door opened!");
            transform.Rotate(0f, 90f, 0f);
            _isOpen = true;
        }
        else
        {
            Debug.Log("Door is already open.");
        }
    }
}
