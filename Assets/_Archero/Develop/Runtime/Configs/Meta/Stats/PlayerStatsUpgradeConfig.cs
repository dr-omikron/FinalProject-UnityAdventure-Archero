using System;
using System.Collections.Generic;
using System.Linq;
using _Archero.Develop.Runtime.Gameplay.Features.StatsFeature;
using _Archero.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace _Archero.Develop.Runtime.Configs.Meta.Stats
{
    [CreateAssetMenu(menuName = "Configs/Meta/PlayerStatsUpgradeConfig", fileName = "PlayerStatsUpgradeConfig")]
    public class PlayerStatsUpgradeConfig : ScriptableObject
    {
        [SerializeField] private List<StatUpgradeCostConfig> _stats = new List<StatUpgradeCostConfig>();

        public StatUpgradeCostConfig GetStatConfig(StatTypes type) => 
            _stats.First(s => s.Type == type);
    }

    [Serializable]
    public class StatUpgradeCostConfig
    {
        [field: SerializeField] public StatTypes Type { get; private set; }
        [field: SerializeField] public List<float> StatValues { get; private set; }
        [field: SerializeField] public CurrencyType CostType { get; private set; } = CurrencyType.Gold;
        [field: SerializeField] public List<int> UpgradeToNextLevelCost { get; private set; }
    }
}
