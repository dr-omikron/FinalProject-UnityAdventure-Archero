using System;
using System.Collections.Generic;
using System.Linq;
using _Archero.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace _Archero.Develop.Runtime.Configs.Meta.Wallet
{
    [CreateAssetMenu(menuName = "Configs/Meta/Wallet/NewStartWalletConfig", fileName = "StartWalletConfig")]
    public class StartWalletConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyConfig> _values;

        public int GetValueFor(CurrencyType currencyType)
            => _values.First(config => config.Type == currencyType).Value;

        [Serializable]
        private class CurrencyConfig
        {
            [field: SerializeField] public CurrencyType Type { get; private set; }
            [field: SerializeField] public int Value { get; private set; }
        }
    }
}
