using UnityEngine;

public interface IActiveAbilityHandler
{
    public void StartAbility(AbilityExtendableEnum abilityExtendableEnum);
    public void FinishAbility();
    public void AbilityIsStillExecuting();
}
