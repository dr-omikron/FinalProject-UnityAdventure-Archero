using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.LootFeature
{
    public class CollectedOnNearToTargetSystem : IInitializableSystem, IUpdateableSystem
    {
        private ReactiveVariable<Entity> _target;
        private Transform _transform;
        private ReactiveVariable<bool> _isCollected;

        public void OnInit(Entity entity)
        {
            _target = entity.CurrentTarget;
            _transform = entity.Transform;
            _isCollected = entity.IsCollected;
        }

        public void OnUpdate(float deltaTime)
        {
            if(_isCollected.Value == false && _target.Value != null)
                if((_target.Value.Transform.position - _transform.position).sqrMagnitude < 0.3f)
                    _isCollected.Value = true;
        }
    }
}
