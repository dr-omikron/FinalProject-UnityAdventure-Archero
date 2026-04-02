using System;
using System.Collections.Generic;
using _Archero.Develop.Runtime.Configs.Gameplay;
using _Archero.Develop.Runtime.Configs.Gameplay.Entities;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.Features.AbilitiesFeature;
using _Archero.Develop.Runtime.Gameplay.Features.AI;
using _Archero.Develop.Runtime.Gameplay.Features.AI.States;
using _Archero.Develop.Runtime.Gameplay.Features.LevelUpFeature;
using _Archero.Develop.Runtime.Gameplay.Features.StatsFeature;
using _Archero.Develop.Runtime.Gameplay.Features.TeamsFeature;
using _Archero.Develop.Runtime.Infrastructure.DI;
using _Archero.Develop.Runtime.Meta.Features.StatsUpgrade;
using _Archero.Develop.Runtime.Utilities.ConfigsManagement;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.MainHero
{
    public class MainHeroFactory
    {
        private readonly DIContainer _container;
        private readonly EntitiesFactory _entitiesFactory;
        private readonly BrainsFactory _brainsFactory;
        private readonly ConfigsProviderService _configsProviderService;
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly StatsUpgradeService _statsUpgradeService;

        public MainHeroFactory(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
            _brainsFactory = _container.Resolve<BrainsFactory>();
            _configsProviderService = _container.Resolve<ConfigsProviderService>();
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _statsUpgradeService = _container.Resolve<StatsUpgradeService>();
        }

        public Entity Create(Vector3 position)
        {
            HeroConfig config = _configsProviderService.GetConfig<HeroConfig>();
            Entity entity = _entitiesFactory.CreateHero(position, config, GetStats());

            entity
                .AddIsMainHero()
                .AddTeam(new ReactiveVariable<Teams>(Teams.MainHero));

            entity
                .AddAbilities()
                .AddSystem(new AbilityOnAddActivatorSystem());

            entity
                .AddLevel(new ReactiveVariable<int>(1))
                .AddExperience()
                .AddCoins()
                .AddSystem(new LevelUpSystem(_configsProviderService.GetConfig<ExperienceForUpgradeLevelConfig>()));

            entity.AddCurrentTarget();

            _brainsFactory.CreateMainHeroBrain(entity, new NearestDamageableTargetSelector(entity));
 
            _entitiesLifeContext.Add(entity);
            return entity;
        }

        private Dictionary<StatTypes, float> GetStats()
        {
            Dictionary<StatTypes, float> stats = new Dictionary<StatTypes, float>();

            foreach (StatTypes statType in Enum.GetValues(typeof(StatTypes)))
                stats.Add(statType, _statsUpgradeService.GetCurrentStatValueFor(statType));

            return stats;
        }
    }
}
