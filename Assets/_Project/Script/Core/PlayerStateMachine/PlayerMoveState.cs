using NF.Main.Core.PlayerStateMachine;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerController playerController, Animator animator)
        : base(playerController, animator) {}

    public override async void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Entering Ability State");

        _animator.CrossFade("Run", 0f);

    }

    public override void Update()
    {
        base.Update();
        if (_playerController.GetMovementInput().sqrMagnitude < 0.1f)
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