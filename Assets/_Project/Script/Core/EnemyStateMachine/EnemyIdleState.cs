using NF.Main.Core.EnemyStateMachine;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(BaseEnemy baseEnemy, Animator animator) : base(baseEnemy, animator)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        //Use this for transitioning between different animator hashes
        _animator.CrossFade(IdleHash, 0.05f);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}