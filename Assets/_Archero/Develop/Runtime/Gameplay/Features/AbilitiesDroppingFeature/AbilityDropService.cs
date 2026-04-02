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

        public List<AbilityDropOption> Drop(int count, Entity entity)
        {
            List<AbilityDropOption> availableAbilities = new List<AbilityDropOption>();

            foreach (AbilityConfig abilitiesConfig in _abilitiesConfigsContainer.AbilitiesConfigs)
            {
                for (int level = 1; level < abilitiesConfig.MaxLevel + 1; level++)
                {
                    if(_abilitiesDroppingRules.IsAvailable(abilitiesConfig, entity, level))
                        availableAbilities.Add(new AbilityDropOption(abilitiesConfig, level));
                }
            }

            List<AbilityDropOption> selectedAbilities = new List<AbilityDropOption>();

            for (int i = 0; i < count; i++)
            {
                AbilityDropOption selectedAbility = availableAbilities[Random.Range(0, availableAbilities.Count)];
                selectedAbilities.Add(selectedAbility);
                availableAbilities.Remove(selectedAbility);
            }

            return selectedAbilities;
        }
    }
}
