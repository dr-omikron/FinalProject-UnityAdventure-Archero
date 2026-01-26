using System;
using System.Collections.Generic;
using System.Linq;
using _Archero.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace _Archero.Develop.Runtime.Configs.Meta.Wallet
{
    [CreateAssetMenu(menuName = "Configs/Meta/Wallet/CurrencyIconsConfig", fileName = "CurrencyIconsConfig")]
    public class CurrencyIconsConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyConfig> _configs;

        public Sprite GetSpriteFor(CurrencyType currencyType)
            => _configs.First(config => config.Type == currencyType).Sprite;

        [Serializable]
        private class CurrencyConfig
        {
            [field: SerializeField] public CurrencyType Type { get; private set; }
            [field: SerializeField] public Sprite Sprite { get; private set; }
        }
    }
}
