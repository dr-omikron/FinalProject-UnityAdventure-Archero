using System;
using System.Collections.Generic;
using _Archero.Develop.Runtime.Configs.Meta.Wallet;
using _Archero.Develop.Runtime.Gameplay.Features.StatsFeature;
using _Archero.Develop.Runtime.Meta.Features.Wallet;
using _Archero.Develop.Runtime.Utilities.ConfigsManagement;

namespace _Archero.Develop.Runtime.Utilities.DataManagement.DataProviders
{
    public class PlayerDataProvider : DataProvider<PlayerData>
    {
        private readonly ConfigsProviderService _configsProviderService;

        public PlayerDataProvider(
            ISaveLoadService saveLoadService, 
            ConfigsProviderService configsProviderService) : base(saveLoadService)
        {
            _configsProviderService = configsProviderService;
        }

        protected override PlayerData GetOriginData()
        {
            return new PlayerData
            {
                WalletData = InitWalletData(),
                CompletedLevels = new List<int>(),
                StatsUpgradeLevels = InitStatsUpgradesLevels()
            };
        }

        private Dictionary<StatTypes, int> InitStatsUpgradesLevels()
        {
            Dictionary<StatTypes, int> statsUpgrades = new Dictionary<StatTypes, int>();

            foreach (StatTypes statType in Enum.GetValues(typeof(StatTypes)))
                statsUpgrades.Add(statType, 1);

            return statsUpgrades;
        }

        private Dictionary<CurrencyType, int> InitWalletData()
        {
            Dictionary<CurrencyType, int> walletData = new Dictionary<CurrencyType, int>();
            StartWalletConfig walletConfig = _configsProviderService.GetConfig<StartWalletConfig>();

            foreach (CurrencyType currencyType in Enum.GetValues(typeof(CurrencyType)))
                walletData[currencyType] = walletConfig.GetValueFor(currencyType);

            return walletData;
        }
    }
}
