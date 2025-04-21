

using UnityEngine;

namespace _Project.Script.Core.EnemyStateMachine
{
    //Handles all logic for when player goes in, out, and during idle state
    public class EnemyDeathState: EnemyBaseState
    {
        public EnemyDeathState(Animator animator) : base(animator)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Enemy Enter Death State");
            //Use this for transitioning between different animator hashes
            _animator.CrossFade(DeathHash, 0.5f);
        }

        public override void Update()
        {
            base.Update();
            
        }

        public override void FixedUpdate()
        {
            
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}