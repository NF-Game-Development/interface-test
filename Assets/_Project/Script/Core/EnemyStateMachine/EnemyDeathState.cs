using NF.Main.Core.EnemyStateMachine;
using UnityEngine;

public class EnemyDeathState: EnemyBaseState
{
    public EnemyDeathState(BaseEnemy baseEnemy, Animator animator) : base(baseEnemy, animator)
    {
    }
        
    public override void OnEnter()
    {
        base.OnEnter();
            
        _animator.CrossFade("Death", 0f);
    }
    public override void OnExit()
    {
        base.OnExit();
        Debug.Log("Exiting Player Idle State");
    }
}