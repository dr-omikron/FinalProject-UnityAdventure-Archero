
using System.Collections.Generic;
using _Archero.Develop.Runtime.Meta.Features.Wallet;

namespace _Archero.Develop.Runtime.Utilities.DataManagement
{
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyType, int> WalletData;
        public List<int> CompletedLevels;
    }
}
