
using UnityEngine;

namespace _Project.Script.Core.EnemyStateMachine
{
    //Handles all logic for when player goes in, out, and during idle state
    public class EnemyIdleState: EnemyBaseState
    {
        public EnemyIdleState(Animator animator) : base(animator)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("idle stte");
            //Use this for transitioning between different animator hashes
            _animator.CrossFade(IdleHash, 0.5f);
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