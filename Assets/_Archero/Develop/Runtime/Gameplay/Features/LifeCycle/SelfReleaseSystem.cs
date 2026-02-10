using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.Gameplay.Features.LifeCycle
{
    public class SelfReleaseSystem : IInitializableSystem, IUpdateableSystem
    {
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private Entity _entity;
        private ReactiveVariable<bool> _isDead;
        private ReactiveVariable<bool> _inDeathProcess;

        public SelfReleaseSystem(EntitiesLifeContext entitiesLifeContext)
        {
            _entitiesLifeContext = entitiesLifeContext;
        }

        public void OnInit(Entity entity)
        {
            _entity = entity;
            _isDead = entity.IsDead;
            _inDeathProcess = entity.InDeathProcess;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_isDead.Value && _inDeathProcess.Value == false)
                _entitiesLifeContext.Release(_entity);
        }
    }
}
