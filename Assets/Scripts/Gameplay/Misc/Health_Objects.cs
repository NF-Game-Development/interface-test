using UnityEngine;

public class Health_Objects : Health
{
    [SerializeField] private IBreakable _breakable;

    public override void OnDeath()
    {
        _breakable.Break();
    }
}
