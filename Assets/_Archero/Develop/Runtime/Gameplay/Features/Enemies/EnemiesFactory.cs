using System;
using _Archero.Develop.Runtime.Configs.Gameplay.Entities;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.Features.AI;
using _Archero.Develop.Runtime.Gameplay.Features.TeamsFeature;
using _Archero.Develop.Runtime.Infrastructure.DI;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.Enemies
{
    public class EnemiesFactory
    {
        private readonly DIContainer _container;
        private readonly EntitiesFactory _entitiesFactory;
        private readonly BrainsFactory _brainsFactory;
        private readonly EntitiesLifeContext _entitiesLifeContext;

        public EnemiesFactory(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
            _brainsFactory = _container.Resolve<BrainsFactory>();
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
        }

        public Entity Create(Vector3 position, EntityConfig config)
        {
            Entity entity;

            switch (config)
            {
                case GhostConfig ghostConfig:
                    entity = _entitiesFactory.CreateGhost(position, ghostConfig);
                    _brainsFactory.CreateGhostBrain(entity);
                    break;

                default:
                    throw new ArgumentException($"Not supported entity type: {config.GetType()}");
            }

            entity.AddTeam(new ReactiveVariable<Teams>(Teams.Enemies));
            _entitiesLifeContext.Add(entity);

            return entity;
        }
    }
}
