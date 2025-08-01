using UnityEngine;
using DG.Tweening;

public class Door : MonoExt, IInteractable
{
    [SerializeField] private GameObject interactableObject;
    [SerializeField] private OpeningValuesScriptableObject _doorOpen;

    private Tween _currentTween;

    private float _openAngle;
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
        _openAngle = _doorOpen.OpenAngle;
        _duration = _doorOpen.Duration;
        _isOpen = _doorOpen.IsOpen;
        
        _closedRotation = interactableObject.transform.localRotation;
        _openedRotation = Quaternion.Euler(0f, _openAngle, 0f) * _closedRotation;
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
