using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Archero.Develop.Runtime.Configs.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Abilities/AbilitiesConfigsContainer", fileName = "AbilitiesConfigsContainer")]
    public class AbilitiesConfigsContainer : ScriptableObject
    {
        [SerializeField] private List<AbilityConfig> _abilitiesConfigs;

        public IReadOnlyList<AbilityConfig> AbilitiesConfigs => _abilitiesConfigs;
        
        public AbilityConfig GetConfigBy(string id) => _abilitiesConfigs.First(config => config.ID == id);
    }
}
