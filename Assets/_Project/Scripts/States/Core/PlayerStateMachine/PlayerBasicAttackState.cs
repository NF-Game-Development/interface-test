using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    public class PlayerBasicAttackState : PlayerBaseState
    {
        private PlayerController _playerController;
        
        public PlayerBasicAttackState(PlayerController playerController, Animator animator) : base(playerController, animator)
        {
            _playerController = playerController;
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            _animator.CrossFade(BasicAttackHash, 0.2f);
            
            _playerController.HandleBasicAttack();
            
        }

        public override void Update()
        {
            base.Update();
            
            var currentState = _animator.GetCurrentAnimatorStateInfo(0);

            // If current animation is BasicAttack and finished
            if (currentState.shortNameHash == BasicAttackHash && currentState.normalizedTime >= 1f)
            {
                _playerController.AssignNewState(PlayerState.Idle);
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
