using System;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.Gameplay.Features.StatsFeature
{
    public class AttackTimeByAttackSpeedSynchronizerSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveVariable<float> _attackPerSecond;

        private ReactiveVariable<float> _attackProcessInitialTime;
        private ReactiveVariable<float> _attackProcessModifiedTime;
        private ReactiveVariable<float> _attackCooldownInitialTime;
        private ReactiveVariable<float> _attackCooldownModifiedTime;
        private ReactiveVariable<float> _attackDelayInitialTime;
        private ReactiveVariable<float> _attackDelayModifiedTime;

        private IDisposable _attackPerSecondChangedDisposable;

        public void OnInit(Entity entity)
        {
            _attackPerSecond = entity.AttackPerSeconds;
            _attackProcessInitialTime = entity.AttackProcessInitialTime;
            _attackProcessModifiedTime = entity.AttackProcessModifiedTime;
            _attackCooldownInitialTime = entity.AttackCooldownInitialTime;
            _attackCooldownModifiedTime = entity.AttackCooldownModifiedTime;
            _attackDelayInitialTime = entity.AttackDelayTime;
            _attackDelayModifiedTime = entity.AttackDelayModifiedTime;

            _attackPerSecondChangedDisposable = _attackPerSecond.Subscribe(OnAttackPerSecondChanged);
            OnAttackPerSecondChanged(0, _attackPerSecond.Value);
        }

        private void OnAttackPerSecondChanged(float arg1, float newAttackPerSecond)
        {
            float totalBaseTime = _attackProcessInitialTime.Value + _attackCooldownInitialTime.Value;
            float targetTotalTime = 1f / newAttackPerSecond;
            float totalTimeRatio = targetTotalTime / totalBaseTime;

            _attackProcessModifiedTime.Value = _attackProcessInitialTime.Value * totalTimeRatio;
            _attackCooldownModifiedTime.Value = _attackCooldownInitialTime.Value * totalTimeRatio;
            _attackDelayModifiedTime.Value = _attackDelayInitialTime.Value * totalTimeRatio;
        }

        public void OnDispose() => _attackPerSecondChangedDisposable.Dispose();
    }
}
