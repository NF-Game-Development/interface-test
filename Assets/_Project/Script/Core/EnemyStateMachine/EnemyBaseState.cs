
using NF.Main.Core;
using UnityEngine;

namespace _Project.Script.Core.EnemyStateMachine
{
    public class EnemyBaseState: BaseState
    {
        protected readonly Animator _animator;

        protected static readonly int IdleHash = Animator.StringToHash("Idle");
        protected static readonly int DeathHash = Animator.StringToHash("Death");

        protected EnemyBaseState(Animator animator)
        {
            _animator = animator;
        }
    }
    
    public enum EnemyState
    {
        Idle,
        Death
    }
}

