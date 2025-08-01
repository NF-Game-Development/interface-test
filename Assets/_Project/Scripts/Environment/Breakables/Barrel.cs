using UnityEngine;

public class Barrel : MonoExt, IDamageable
{
    [SerializeField] private BreakableValuesScriptableObject _breakableValuesScriptableObject;
    
    private float _currentHealth;
    private bool _isBroken;
    
    private void Awake()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        InitializeBreakable();
    }

    private void InitializeBreakable()
    {
        _currentHealth = _breakableValuesScriptableObject.ObjectHealth;
        _isBroken = false;
    }

    public void ApplyDamage(float damageAmount)
    {
        if (_isBroken) return;

        _currentHealth -= damageAmount;

        if (_currentHealth <= 0)
        {
            Break();
        }
    }
    
    private void Break()
    {
        _isBroken = true;
        Debug.Log($"{name} is broken!");
        Destroy(gameObject);
    }
}
