using NF.Main.Core.PlayerStateMachine;
using UnityEngine;

public class PlayerAbilityState : PlayerBaseState
{
    public PlayerAbilityState(PlayerController playerController, Animator animator)
        : base(playerController, animator) {}

    public override async void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Entering Ability State");

        var ability = _playerController.GetPendingAbility();

        if (!string.IsNullOrEmpty(ability.ID))
        {
            _animator.CrossFade(ability.ID, 0f);
        }

        await ability.OnTriggerAbility(_playerController.GetCenterTransform(), _playerController.GetAbilityParameterHandler());

    }

    public override void Update()
    {
        base.Update();
        if (!_playerController.GetAbilityParameterHandler().IsAnAbilityExecuting)
        {
            _playerController.PlayerState = PlayerState.Idle;
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        Debug.Log("Exiting Ability State");
    }
}