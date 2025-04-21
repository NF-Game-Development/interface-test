
using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    //Handles all logic for when player goes in, out, and during idle state
    public class PlayerAbility3State: PlayerBaseState
    {
        public PlayerAbility3State(PlayerController playerController, Animator animator) : base(playerController, animator)
        {
            SetAbility3Hash(playerController.GetAbiliyAnimationDictionary());
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            //Use this for transitioning between different animator hashes
            _animator.CrossFade(Ability3Hash, 0.5f);
            //Debug.Log("enter abilit3");

        }

        public override void Update()
        {
            base.Update();
            //Debug.Log("abilit3");
            
            
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