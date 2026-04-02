using _Archero.Develop.Runtime.Configs.Abilities;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.Features.StatsFeature;

namespace _Archero.Develop.Runtime.Gameplay.Features.AbilitiesFeature.Abilities
{
    public class StatChangeAbility : Ability
    {
        private readonly Entity _entity;
        private readonly StatChangeAbilityConfig _config;

        public StatChangeAbility(
            Entity entity, 
            StatChangeAbilityConfig config, 
            int currentLevel) : base(config.ID, currentLevel, config.MaxLevel)
        {
            _entity = entity;
            _config = config;
        }

        public override void Activate()
        {
            _entity.StatsEffects.Add(new StatsEffect(_config.StatType, _config.GetApplyEffect()));
        }
    }
}
