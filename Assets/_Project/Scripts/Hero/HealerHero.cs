using UnityEngine;

public class HealerHero : BaseHero, IHealer
{
    
    public override void Attack(IDamageable damageable)
    {
        //Dont Attack
    }

    public void Heal(BaseHero baseHero)
    {
        throw new System.NotImplementedException();
    }
}
