using System;
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

        private ReactiveVariable<float> _attackProcessInitialTime;
        private ReactiveVariable<float> _attackProcessModifiedTime;

        private IDisposable _attackProcessTimeChangedDisposable;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }
        protected override void OnEntityStartedWork(Entity entity)
        {
            _attackProcessInitialTime = entity.AttackProcessInitialTime;
            _attackProcessModifiedTime = entity.AttackProcessModifiedTime;

            _attackProcessTimeChangedDisposable = _attackProcessModifiedTime.Subscribe(OnAttackProcessTimeChanged);
            OnAttackProcessTimeChanged(0, _attackProcessModifiedTime.Value);
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);
            _attackProcessTimeChangedDisposable.Dispose();
        }

        private void OnAttackProcessTimeChanged(float arg1, float currentAttackProcessTime)
        {
            _animator.SetFloat(_attackAnimationSpeedMultiplierKey, _attackProcessInitialTime.Value / currentAttackProcessTime);
        }
    }
}
