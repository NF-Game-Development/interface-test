
using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    public class PlayerBaseState: BaseState
    {
        protected readonly PlayerController _playerController;
        protected readonly Animator _animator;

        protected static readonly int IdleHash = Animator.StringToHash("Idle");
        protected static readonly int MoveHash = Animator.StringToHash("Running");
        protected static readonly int AttackHash = Animator.StringToHash("Attack");
        protected static readonly int HitHash = Animator.StringToHash("Hit");
        protected static readonly int DeathHash = Animator.StringToHash("Death");
        
        protected static  int Ability1Hash;
        protected static  int Ability2Hash;
        protected static  int Ability3Hash;
        
        protected PlayerBaseState(PlayerController playerController, Animator animator)
        {
            _playerController = playerController;
            _animator = animator;
        }

        public void SetAbility1Hash(AbilityAnimationDictionary abilityAnimationDictionary)
        {
            Ability1Hash = Animator.StringToHash(abilityAnimationDictionary.AnimationDictionary[1]);
        }
        
        public void SetAbility2Hash(AbilityAnimationDictionary abilityAnimationDictionary)
        {
            Ability2Hash = Animator.StringToHash(abilityAnimationDictionary.AnimationDictionary[2]);
        }
        
        public void SetAbility3Hash(AbilityAnimationDictionary abilityAnimationDictionary)
        {
            Ability3Hash = Animator.StringToHash(abilityAnimationDictionary.AnimationDictionary[3]);
        }
    }
    
    public enum PlayerState
    {
        Idle,
        Moving,
        Attacking,
        Ability1,
        Ability2,
        Ability3,
        Hit,
        Death
    }
}

