using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    public class PlayerMoveState : PlayerBaseState
    {
        public PlayerMoveState(PlayerController playerController, Animator animator) : base(playerController, animator)
        {
        
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _animator.CrossFade(MoveHash, 0.2f);
            
            Debug.Log("Player Move State");
        }

        public override void Update()
        {
            base.Update();
            Debug.Log("Player is Moving");
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("Player Exiting MoveState");
        }
    }
}

