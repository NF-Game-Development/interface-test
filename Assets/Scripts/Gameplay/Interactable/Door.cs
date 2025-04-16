using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
public class Door : MonoExt, IInteractable
{
    [SerializeField] private bool _isOpen = false;
    [SerializeField] private bool _isInteractable = true;
    [SerializeField] private GameObject _door;
    [SerializeField] private float _openDoorFinalRotation;
    [SerializeField] private float _openDoorTime;
    
    
    private void Awake()
    {
        //Initialize mono extension
        Initialize();
    }
    private void Start()
    {
        //Events
        OnSubscriptionSet();
    }
    
    public override void Initialize()
    {
        base.Initialize();
    }
    
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
    }

    private async UniTask OpenDoorAnimation()
    {
        _isInteractable = false;
        float remainingTime = 0;
        float rotationAlpha;
        float currentRotation;
        while (remainingTime < _openDoorTime)
        {
            rotationAlpha = remainingTime / _openDoorTime;
            await UniTask.Yield(PlayerLoopTiming.Update);
            currentRotation = Mathf.SmoothStep(0f, _openDoorFinalRotation, rotationAlpha);
            _door.transform.localRotation = Quaternion.Euler(0, currentRotation, 0);
            remainingTime += Time.deltaTime;
        } 
        _isOpen = true;
        _isInteractable = true;
    }
    
    private async UniTask CloseDoorAnimation()
    {
        _isInteractable = false;
        float remainingTime = 0;
        float rotationAlpha;
        float currentRotation;
        while (remainingTime < _openDoorTime)
        {
            rotationAlpha = remainingTime / _openDoorTime;
            await UniTask.Yield(PlayerLoopTiming.Update);
            currentRotation = Mathf.SmoothStep(_openDoorFinalRotation, 0f , rotationAlpha);
            _door.transform.localRotation = Quaternion.Euler(0, currentRotation, 0);
            remainingTime += Time.deltaTime;
        }
        _isOpen = false;
        _isInteractable = true;
    }

    private void OpenCloseDoor()
    {
        if (_isOpen == false)
        {
            OpenDoorAnimation();
        }
        else if (_isOpen == true)
        {
            CloseDoorAnimation();
        }
    }

    [Button]
    public void Interact()
    {
        if(_isInteractable == false)
            return;
        
        OpenCloseDoor();
    }
}
