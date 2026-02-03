using _Archero.Develop.Runtime.Gameplay.Features.MovementFeature;
using _Archero.Develop.Runtime.Infrastructure.DI;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.EntitiesCore
{
    public class EntitiesFactory
    {
        private readonly DIContainer _container;
        private readonly EntitiesLifeContext _entitiesLifeContext;

        public EntitiesFactory(DIContainer container)
        {
            _container = container;
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
        }

        public Entity CreateEntity()
        {
            Entity entity = CreateEmpty();

            entity
                .AddComponent(new MoveDirection { Value = new ReactiveVariable<Vector3>(Vector3.forward) })
                .AddComponent(new MoveSpeed { Value = new ReactiveVariable<float>(10) });

            entity.AddSystem(new MovementSystem());

            _entitiesLifeContext.Add(entity);

            return entity;
        }
        
        private Entity CreateEmpty() => new Entity();
    }
}
