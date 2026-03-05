using System;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.Attack
{
    public class InstantAttackView : EntityView
    {

        private readonly int _isAttackKey = Animator.StringToHash("IsAttacking");

        [SerializeField] private Animator _animator;

        private IReadOnlyVariable<bool> _inAttackProcess;
        private IDisposable _isAttackChangedDisposable;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityStartedWork(Entity entity)
        {
            _inAttackProcess = entity.InAttackProcess;
            _isAttackChangedDisposable = _inAttackProcess.Subscribe(OnAttackProcessChanged);
            UpdateInAttack(_inAttackProcess.Value);
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);
            _isAttackChangedDisposable?.Dispose();
        }

        private void OnAttackProcessChanged(bool oldValue, bool newValue) => UpdateInAttack(newValue);

        private void UpdateInAttack(bool value) => _animator.SetBool(_isAttackKey, value);
    }
}
