using UnityEngine;

namespace NF.Main.Core.EnemyStateMachine
{
    public class EnemyBaseState: BaseState
    {
        protected readonly BaseEnemy _baseEnemy;
        protected readonly Animator _animator;

        protected static readonly int IdleHash = Animator.StringToHash("Idle");
        protected static readonly int DeathHash = Animator.StringToHash("Death");
        
        protected EnemyBaseState(BaseEnemy baseEnemy, Animator animator)
        {
            _baseEnemy = baseEnemy;
            _animator = animator;
        }
    }
    
    public enum EnemyState
    {
        Idle,
        Dead,
    }
}