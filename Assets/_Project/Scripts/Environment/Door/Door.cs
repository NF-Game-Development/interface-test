using UnityEngine;
using DG.Tweening;

public class Door : MonoExt, IInteractable
{
    [SerializeField] private GameObject interactableObject;
    [SerializeField] private OpeningValuesScriptableObject _doorOpen;

    private Tween _currentTween;

    private float _openAngleX;
    private float _openAngleY;
    private float _openAngleZ;
    private float _duration;
    private bool _isOpen;
    
    private Quaternion _closedRotation;
    private Quaternion _openedRotation;

    private void Awake()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        
        InitializeValues();
    }

    private void InitializeValues()
    {
        _openAngleX = _doorOpen.OpenAngleX;
        _openAngleY = _doorOpen.OpenAngleY;
        _openAngleZ = _doorOpen.OpenAngleZ;
        _duration = _doorOpen.Duration;
        _isOpen = _doorOpen.IsOpen;
        
        _closedRotation = interactableObject.transform.localRotation;
        _openedRotation = Quaternion.Euler(_openAngleX, _openAngleY, _openAngleZ) * _closedRotation;
    }

    public void Interact()
    {
        if (_currentTween != null && _currentTween.IsActive() && _currentTween.IsPlaying()) return;

        _isOpen = !_isOpen;

        Quaternion targetRotation = _isOpen ? _openedRotation : _closedRotation;

        _currentTween = interactableObject.transform
            .DOLocalRotateQuaternion(targetRotation, _duration)
            .SetEase(Ease.OutQuad)
            .SetAutoKill(true); 
    }
}
