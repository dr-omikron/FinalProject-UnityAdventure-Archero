using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Mono;
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
        private readonly MonoEntityFactory _monoEntityFactory;

        public EntitiesFactory(DIContainer container)
        {
            _container = container;
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _monoEntityFactory = _container.Resolve<MonoEntityFactory>();
        }

        public Entity CreateEntity(Vector3 position)
        {
            Entity entity = CreateEmpty();

            _monoEntityFactory.Create(entity, position, "Entities/TestEntity");

            entity
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(10));

            entity.AddSystem(new RigidbodyMovementSystem());

            _entitiesLifeContext.Add(entity);

            return entity;
        }

        private Entity CreateEmpty() => new Entity();
    }
}
