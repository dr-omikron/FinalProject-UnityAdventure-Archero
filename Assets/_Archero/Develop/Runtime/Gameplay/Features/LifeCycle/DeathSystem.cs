using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.LifeCycle
{
    public class DeathSystem : IInitializableSystem, IUpdateableSystem
    {
        private ReactiveVariable<bool> _isDead;
        private ReactiveVariable<float> _currentHealth;

        public void OnInit(Entity entity)
        {
            _isDead = entity.IsDead;
            _currentHealth = entity.CurrentHealth;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_isDead.Value)
                return;

            if (_currentHealth.Value <= 0)
            {
                _isDead.Value = true;
                Debug.Log("DeathSystem: Dead");
            }
        }
    }
}
