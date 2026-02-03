using _Archero.Develop.Runtime.Gameplay.Features.MovementFeature;
using _Archero.Develop.Runtime.Infrastructure.DI;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.EntitiesCore
{
    public class EntitiesFactory
    {
        private readonly DIContainer _container;

        public EntitiesFactory(DIContainer container)
        {
            _container = container;
        }

        public Entity CreateEntity()
        {
            Entity entity = CreateEmpty();

            entity
                .AddComponent(new MoveDirection { Value = new ReactiveVariable<Vector3>(Vector3.forward) })
                .AddComponent(new MoveSpeed { Value = new ReactiveVariable<float>(10) });

            return entity;
        }
        
        private Entity CreateEmpty() => new Entity();
    }
}
