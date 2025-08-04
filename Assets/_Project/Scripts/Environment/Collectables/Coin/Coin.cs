using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;

public class Coin : Collectables
{
    [SerializeField] private CoinScriptableObject _coinScriptableObject;
    [SerializeField] private CollectAnimationCurve _coinAnimationCurve;

    private AnimationCurve _moveCurve;
    private AnimationCurve _scaleCurve;
    private float _duration;
    
    private float _coinValue;
    private Tween _currentTween;
    private Transform _player;

    private Vector3 _startScale;
    
    private void Awake()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        
        InitializeSettings();
        InitializeCollectAnimationCurve();
        InitializePlayerTag();
    }

    private void InitializeSettings()
    {
        _coinValue = _coinScriptableObject.CoinValue;
        _startScale = transform.localScale;
    }

    private void InitializeCollectAnimationCurve()
    {
        _moveCurve = _coinAnimationCurve.MoveCurve;
        _scaleCurve = _coinAnimationCurve.ScaleCurve;
        _duration = _coinAnimationCurve.Duration;
    }

    private void InitializePlayerTag()
    {
        _player = GameObject.FindWithTag("Player")?.transform;
    }
    
    public override void Collect()
    {
        if (_player == null || (_currentTween?.IsActive() ?? false)) return;

        Vector3 startPosition = transform.position;
        Vector3 playerPosition = _player.position;

        float elapsed = 0f;

        _currentTween = DOTween.To(() => elapsed, x => elapsed = x, _duration, _duration)
            .OnUpdate(() =>
            {
                float normalizedTime = elapsed / _duration;

                float moveValue = _moveCurve.Evaluate(normalizedTime);
                float scaleValue = _scaleCurve.Evaluate(normalizedTime);

                transform.position = Vector3.Lerp(startPosition, playerPosition, moveValue);
                transform.localScale = Vector3.Lerp(_startScale, Vector3.zero, scaleValue);
            })
            .OnComplete(() =>
            {
                Debug.Log($"Collected a coin worth {_coinValue}!");
                Destroy(gameObject);
            })
            .SetEase(Ease.Linear)
            .SetAutoKill(true);
    }
}
