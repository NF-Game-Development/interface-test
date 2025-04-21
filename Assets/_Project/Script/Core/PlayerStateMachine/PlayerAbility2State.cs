
using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    //Handles all logic for when player goes in, out, and during idle state
    public class PlayerAbility2State: PlayerBaseState
    {
        public PlayerAbility2State(PlayerController playerController, Animator animator) : base(playerController, animator)
        {
            SetAbility2Hash(playerController.GetAbiliyAnimationDictionary());
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            //Use this for transitioning between different animator hashes
            _animator.CrossFade(Ability2Hash, 0.5f);
            //Debug.Log("enter abilit2");

        }

        public override void Update()
        {
            base.Update();
            //Debug.Log("abilit2");
            
            
        }

        public override void FixedUpdate()
        {
            
        }

        public override void OnExit()
        {
            base.OnExit();
            //Debug.Log("Exiting ability1 Idle State");
        }
    }
}