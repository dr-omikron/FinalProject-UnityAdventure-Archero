using System;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.LifeCycle
{
    public class DeadView : EntityView
    {
        private readonly int _isDeadKey = Animator.StringToHash("IsDead");

        [SerializeField] private Animator _animator;

        private IReadOnlyVariable<bool> _isDead;
        private IDisposable _isDeadChangedDisposable;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityStartedWork(Entity entity)
        {
            _isDead = entity.IsDead;
            _isDeadChangedDisposable = _isDead.Subscribe(OnIsDeadChanged);
            UpdateIsDead(_isDead.Value);
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);
            _isDeadChangedDisposable?.Dispose();
        }

        private void OnIsDeadChanged(bool oldValue, bool newValue) => UpdateIsDead(newValue);

        private void UpdateIsDead(bool value) => _animator.SetBool(_isDeadKey, value);
    }
}
