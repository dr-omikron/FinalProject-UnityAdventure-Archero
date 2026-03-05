using System;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class WalkingView : EntityView
    {
        private readonly int _isMovingKey = Animator.StringToHash("IsWalking");

        [SerializeField] private Animator _animator;

        private IReadOnlyVariable<bool> _isMoving;
        private IDisposable _isMovingChangedDisposable;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityStartedWork(Entity entity)
        {
            _isMoving = entity.IsMoving;
            _isMovingChangedDisposable = _isMoving.Subscribe(OnIsMovingChanged);
            UpdateIsMoving(_isMoving.Value);
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);
            _isMovingChangedDisposable?.Dispose();
        }

        private void OnIsMovingChanged(bool oldValue, bool newValue) => UpdateIsMoving(newValue);

        private void UpdateIsMoving(bool value) => _animator.SetBool(_isMovingKey, value);
    }
}
