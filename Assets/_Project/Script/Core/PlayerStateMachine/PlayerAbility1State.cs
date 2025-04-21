
using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    //Handles all logic for when player goes in, out, and during idle state
    public class PlayerAbility1State: PlayerBaseState
    {
        public PlayerAbility1State(PlayerController playerController, Animator animator) : base(playerController, animator)
        {
            SetAbility1Hash(playerController.GetAbiliyAnimationDictionary());
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            //Use this for transitioning between different animator hashes
            _animator.CrossFade(Ability1Hash, 0.5f);
            //Debug.Log("enter abilit1");

        }

        public override void Update()
        {
            base.Update();
            //Debug.Log("abilit1");
            
            
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