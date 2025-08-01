using UnityEngine;

public class WarriorHero : BaseHero
{
    //FOR TESTING PURPOSES
    //TODO: Use the ClassTypeDataScriptable for the Damage amount
    
    [SerializeField] private float _attackRadius = 1.5f;
    [SerializeField] private float _damage = 10;
    
    public override void Initialize()
    {
        base.Initialize();
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
        Collider[] hits = Physics.OverlapSphere(transform.position, _attackRadius);

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
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }

}
