using UnityEngine;

public class WarriorHero : BaseHero
{
    [SerializeField] private Transform _hitLocation;
    private float _attackRadius;
    private float _damage;
    
    public override void Initialize()
    {
        base.Initialize();

        InitializeDamageValues();
    }

    private void InitializeDamageValues()
    {
        _damage = HeroScriptableObject.BaseAttack;
        _attackRadius = HeroScriptableObject.MeleeRadius;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PerformAttack();
        }
    }
    
    private void PerformAttack()
    {
        Collider[] hits = Physics.OverlapSphere(_hitLocation.transform.position, _attackRadius);

        foreach (var hit in hits)
        {
            if (hit.transform.root.TryGetComponent<IDamageable>(out var damageable))
            {
                Attack(damageable);
            }
        }
    }
    
    public override void Attack(IDamageable damageable)
    {
        damageable.ApplyDamage(_damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_hitLocation.transform.position, _attackRadius);
    }

}
