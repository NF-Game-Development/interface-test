using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    public class PlayerMoveState : PlayerBaseState
    {
        private PlayerController _playerController;
        
        public PlayerMoveState(PlayerController playerController, Animator animator) : base(playerController, animator)
        {
            _playerController = playerController;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _animator.CrossFade(MoveHash, 0.2f);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            _playerController.HandleMovement();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}

