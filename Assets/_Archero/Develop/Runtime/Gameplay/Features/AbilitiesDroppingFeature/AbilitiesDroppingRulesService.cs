using System.Collections.Generic;
using System.Linq;
using _Archero.Develop.Runtime.Configs.Abilities;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.Features.StatsFeature;

namespace _Archero.Develop.Runtime.Gameplay.Features.AbilitiesDroppingFeature
{
    public class AbilitiesDroppingRulesService
    {
        public bool IsAvailable(AbilityConfig config, Entity entity, int abilityLevel)
        {
            if (config.IsUpgradable())
            {
                    if (entity.Abilities.Elements.Any(ability => 
                            ability.ID == config.ID
                            && ability.CurrentLevel.Value + abilityLevel > ability.MaxLevel))
                    {
                        return false;
                    }
            }

            switch (config)
            {
                case StatChangeAbilityConfig statChange:
                    return entity.TryGetModifiedStats(out Dictionary<StatTypes, float> modifiedStats)
                           && modifiedStats.ContainsKey(statChange.StatType);
            }

            return true;
        }
    }
}
