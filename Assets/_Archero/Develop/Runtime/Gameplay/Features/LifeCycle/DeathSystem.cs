using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Archero.Develop.Runtime.Utilities.Conditions;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.LifeCycle
{
    public class DeathSystem : IInitializableSystem, IUpdateableSystem
    {
        private ReactiveVariable<bool> _isDead;
        private ICompositeCondition _mustDie;

        public void OnInit(Entity entity)
        {
            _isDead = entity.IsDead;
            _mustDie = entity.MustDie;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_isDead.Value)
                return;

            if (_mustDie.Evaluate())
                _isDead.Value = true;
        }
    }
}
