using System.Collections.Generic;
using _Archero.Develop.Runtime.Meta.Features.Wallet;

namespace _Archero.Develop.Runtime.Utilities.DataManagement.DataProviders
{
    public class PlayerDataProvider : DataProvider<PlayerData>
    {
        public PlayerDataProvider(ISaveLoadService saveLoadService) : base(saveLoadService)
        {
        }

        protected override PlayerData GetOriginData()
        {
            return new PlayerData
            {
                WalletData = InitWalletData()
            };
        }

        private Dictionary<CurrencyType, int> InitWalletData()
        {
            Dictionary<CurrencyType, int> walletData = new Dictionary<CurrencyType, int>
            {
                { CurrencyType.Gold, 100 },
                { CurrencyType.Diamond, 20 }
            };

            return walletData;
        }
    }
}
