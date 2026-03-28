using System.Collections.Generic;
using System.Linq;
using _Archero.Develop.Runtime.Configs.Abilities;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.AbilitiesDroppingFeature
{
    public class AbilityDropService
    {
        private readonly AbilitiesConfigsContainer _abilitiesConfigsContainer;
        private readonly AbilitiesDroppingRulesService _abilitiesDroppingRules;

        public AbilityDropService(
            AbilitiesConfigsContainer abilitiesConfigsContainer, 
            AbilitiesDroppingRulesService abilitiesDroppingRules)
        {
            _abilitiesConfigsContainer = abilitiesConfigsContainer;
            _abilitiesDroppingRules = abilitiesDroppingRules;
        }

        public List<AbilityConfig> Drop(int count, Entity entity)
        {
            List<AbilityConfig> availableAbilities
                = new List<AbilityConfig>(_abilitiesConfigsContainer
                    .AbilitiesConfigs
                    .Where(abilityOption => _abilitiesDroppingRules.IsAvailable(abilityOption, entity)));

            List<AbilityConfig> selectedAbilities = new List<AbilityConfig>();

            for (int i = 0; i < count; i++)
            {
                AbilityConfig selectedAbility = availableAbilities[Random.Range(0, availableAbilities.Count)];
                selectedAbilities.Add(selectedAbility);
                availableAbilities.Remove(selectedAbility);
            }

            return selectedAbilities;
        }
    }
}
