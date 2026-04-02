using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Archero.Develop.Runtime.Configs.Loot
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Loot/LootListConfig", fileName = "LootListConfig")]
    public class LootListConfig : ScriptableObject
    {
        [SerializeField] private List<LootConfig> _lootConfigs;
        public IReadOnlyList<LootConfig> LootConfigs => _lootConfigs;
        public LootConfig GetLootByID(string id) => _lootConfigs.First(loot => loot.ID == id);
    }
}
