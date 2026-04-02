using System.Collections.Generic;
using System.Linq;
using _Archero.Develop.Runtime.Configs.Meta.Stats;
using _Archero.Develop.Runtime.Gameplay.Features.StatsFeature;
using _Archero.Develop.Runtime.Meta.Features.Wallet;
using _Archero.Develop.Runtime.Utilities.ConfigsManagement;
using _Archero.Develop.Runtime.Utilities.DataManagement;
using _Archero.Develop.Runtime.Utilities.DataManagement.DataProviders;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.Meta.Features.StatsUpgrade
{
    public class StatsUpgradeService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly ConfigsProviderService _configsProviderService;
        private readonly Dictionary<StatTypes, ReactiveVariable<int>> _statLevels = new Dictionary<StatTypes, ReactiveVariable<int>>();

        public StatsUpgradeService(PlayerDataProvider playerDataProvider, ConfigsProviderService configsProviderService)
        {
            _configsProviderService = configsProviderService;

            playerDataProvider.RegisterReader(this);
            playerDataProvider.RegisterWriter(this);
        }

        public List<StatTypes> AvailableStats() => _statLevels.Keys.ToList();
        public IReadOnlyVariable<int> GetStatLevelFor(StatTypes statType) => _statLevels[statType];

        private PlayerStatsUpgradeConfig PlayerStatsByLevelConfig => _configsProviderService.GetConfig<PlayerStatsUpgradeConfig>();

        public float GetCurrentStatValueFor(StatTypes statType) 
            => PlayerStatsByLevelConfig.GetStatConfig(statType).StatValues[_statLevels[statType].Value - 1];

        public CurrencyType GetUpgradeCostTypeFor(StatTypes statType)
            => PlayerStatsByLevelConfig.GetStatConfig(statType).CostType;

        public bool TryGetStatValueForNextLevel(StatTypes statType, out float statValue)
        {
            StatUpgradeCostConfig statData = PlayerStatsByLevelConfig.GetStatConfig(statType);

            if (statData.StatValues.Count <= _statLevels[statType].Value)
            {
                statValue = 0;
                return false;
            }

            statValue = statData.StatValues[_statLevels[statType].Value];
            return true;
        }

        public bool TryGetUpgradeCostFor(StatTypes statType, out CurrencyType costType, out int cost)
        {
            StatUpgradeCostConfig statData = PlayerStatsByLevelConfig.GetStatConfig(statType);

            if (statData.UpgradeToNextLevelCost.Count <= _statLevels[statType].Value - 1)
            {
                costType = default;
                cost = 0;
                return false;
            }

            costType = statData.CostType;
            cost = statData.UpgradeToNextLevelCost[_statLevels[statType].Value - 1];
            return true;
        }

        public bool TryUpgradeStat(StatTypes statType)
        {
            StatUpgradeCostConfig statData = PlayerStatsByLevelConfig.GetStatConfig(statType);
            
            if (statData.UpgradeToNextLevelCost.Count <= _statLevels[statType].Value  - 1)
                return false;

            _statLevels[statType].Value++;
            return true;
        }
        
        public void ReadFrom(PlayerData data)
        {
            foreach (KeyValuePair<StatTypes, int> statLevel in data.StatsUpgradeLevels)
            {
                if(_statLevels.ContainsKey(statLevel.Key))
                    _statLevels[statLevel.Key].Value = statLevel.Value;
                else
                    _statLevels.Add(statLevel.Key, new ReactiveVariable<int>(statLevel.Value));
            }
        }

        public void WriteTo(PlayerData data)
        {
            foreach (KeyValuePair<StatTypes, ReactiveVariable<int>> stat in _statLevels)
            {
                if(data.StatsUpgradeLevels.ContainsKey(stat.Key))
                    data.StatsUpgradeLevels[stat.Key] = stat.Value.Value;
                else
                    data.StatsUpgradeLevels.Add(stat.Key, stat.Value.Value);
            }
        }
    }
}
