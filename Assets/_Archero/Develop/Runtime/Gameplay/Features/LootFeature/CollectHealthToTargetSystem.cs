using System;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.Gameplay.Features.LootFeature
{
    public class CollectHealthToTargetSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveVariable<Entity> _target;
        private ReactiveVariable<float> _health;
        private ReactiveVariable<bool> _isCollected;
        
        private IDisposable _collectedChangeDisposable;

        public void OnInit(Entity entity)
        {
            _target = entity.CurrentTarget;
            _health = entity.CurrentHealth;
            _isCollected = entity.IsCollected;

            _collectedChangeDisposable = _isCollected.Subscribe(OnIsCollectedChange);
        }

        private void OnIsCollectedChange(bool arg1, bool isCollected)
        {
            if(isCollected)
            {
                ReactiveVariable<float> currentHealth = _target.Value.CurrentHealth;
                ReactiveVariable<float> maxHealth = _target.Value.MaxHealth;

                if (currentHealth.Value + _health.Value > maxHealth.Value)
                {
                    currentHealth.Value = maxHealth.Value;
                    return;
                }

                currentHealth.Value += _health.Value;
            }
        }

        public void OnDispose() => _collectedChangeDisposable.Dispose();
    }
}
