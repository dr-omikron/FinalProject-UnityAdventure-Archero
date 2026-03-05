using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.Attack
{
    public class InstantAttackAnimationSpeedView : EntityView
    {
        private readonly int _attackAnimationSpeedMultiplierKey = Animator.StringToHash("AttackAnimationSpeedMultiplier");

        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationClip _animationClip;

        private ReactiveVariable<float> _attackProcessTime;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }
        protected override void OnEntityStartedWork(Entity entity)
        {
            _attackProcessTime = entity.AttackProcessInitialTime;
            _animator.SetFloat(_attackAnimationSpeedMultiplierKey, _animationClip.length / _attackProcessTime.Value);
        }
    }
}
