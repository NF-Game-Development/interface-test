
using System;
using Sirenix.OdinInspector;
using TMPro;
using UniRx;
using UnityEngine;

public class Health : MonoExt, IDamageable
{
    [SerializeField] private Stat MaxHealth;
    [SerializeField] private TMP_Text _healthText;

    public float MaxHP => MaxHealth.Value;
    public float HP;

    public Subject<Unit> OnDeathEvent;

    private void Awake()
    {
        OnDeathEvent = new Subject<Unit>();
        HP = MaxHP;
    }
    
    [Button]
    public void ApplyDamage(float damageValue)
    {
        HP -= damageValue;

        if (HP <= 0)
            OnDeath();
        
        UpdateHpText((int)HP);
    }

    public virtual void OnDeath()
    {
        OnDeathEvent.OnNext(Unit.Default);
    }

    public void SetMaxHealth(Stat newMaxHealth)
    {
        MaxHealth = newMaxHealth;
        HP = MaxHP;
        UpdateHpText((int)HP);
    }

    protected void UpdateHpText(int newHpText)
    {
        if(_healthText == null)
            return;
        
        _healthText.text = newHpText.ToString();
    }
}