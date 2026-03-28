using System;
using _Archero.Develop.Runtime.Configs.Abilities;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.Features.AbilitiesFeature.Abilities;
using _Archero.Develop.Runtime.Infrastructure.DI;

namespace _Archero.Develop.Runtime.Gameplay.Features.AbilitiesFeature
{
    public class AbilityFactory
    {
        private readonly DIContainer _container;

        public AbilityFactory(DIContainer container)
        {
            _container = container;
        }

        public Ability CreateAbilityFor(Entity entity, AbilityConfig config)
        {
            switch (config)
            {
                case StatChangeAbilityConfig changeAbilityConfig:
                    return new StatChangeAbility(entity, changeAbilityConfig);

                default:
                   throw new ArgumentException("Invalid ability config");
            }
        }
    }
}
