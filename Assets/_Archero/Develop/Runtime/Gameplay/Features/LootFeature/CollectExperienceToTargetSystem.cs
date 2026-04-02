using System;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.Gameplay.Features.LootFeature
{
    public class CollectExperienceToTargetSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveVariable<Entity> _target;
        private ReactiveVariable<float> _experience;
        private ReactiveVariable<bool> _isCollected;
        
        private IDisposable _collectedChangeDisposable;

        public void OnInit(Entity entity)
        {
            _target = entity.CurrentTarget;
            _experience = entity.Experience;
            _isCollected = entity.IsCollected;

            _collectedChangeDisposable = _isCollected.Subscribe(OnIsCollectedChange);
        }

        private void OnIsCollectedChange(bool arg1, bool isCollected)
        {
            if(isCollected)
                _target.Value.Experience.Value += _experience.Value;
        }

        public void OnDispose() => _collectedChangeDisposable.Dispose();
    }
}
