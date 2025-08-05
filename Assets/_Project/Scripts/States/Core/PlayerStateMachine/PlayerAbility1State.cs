using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    public class PlayerAbility1State : PlayerBaseState
    {
        public PlayerAbility1State(PlayerController playerController, Animator animator) : base(playerController, animator)
        {
            SetAbility1Hash(playerController.GetAbilityAnimationDictionary());
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            //Use this for transitioning between different animator hashes
            _animator.CrossFade(Ability1Hash, 0.5f);
            
            Debug.LogWarning("Ability 1: Entered");
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

}
