using System;
using System.Collections.Generic;
using System.Globalization;
using _Archero.Develop.Runtime.Configs.Meta.Stats;
using _Archero.Develop.Runtime.Configs.Meta.Wallet;
using _Archero.Develop.Runtime.Gameplay.Features.StatsFeature;
using _Archero.Develop.Runtime.Meta.Features.StatsUpgrade;
using _Archero.Develop.Runtime.Meta.Features.Wallet;
using _Archero.Develop.Runtime.UI.Core;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.UI.StatsUpgradePopup
{
    public class UpgradableStatPresenter : IPresenter
    {
        private readonly UpgradableStatView _view;
        private readonly StatsViewConfig _statViewConfig;
        private readonly StatsUpgradeService _statUpgradeService;
        private readonly WalletService _walletService;
        private readonly StatTypes _statType;
        private readonly CurrencyIconsConfig _currencyIconsConfig;

        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        public UpgradableStatPresenter(
            UpgradableStatView view, 
            StatTypes statType, 
            StatsViewConfig statViewConfig, 
            StatsUpgradeService statUpgradeService, 
            WalletService walletService, 
            CurrencyIconsConfig currencyIconsConfig)
        {
            _view = view;
            _statType = statType;
            _statViewConfig = statViewConfig;
            _statUpgradeService = statUpgradeService;
            _walletService = walletService;
            _currencyIconsConfig = currencyIconsConfig;
        }

        public UpgradableStatView View => _view;

        public void Initialize()
        {
            StatViewConfig statsShowData = _statViewConfig.GetStatViewData(_statType);
            _view.Initialize(statsShowData.Name, statsShowData.Sprite, GetStatValueText());

            UpdateBuyButtonState();

            _view.BuyButtonView.Clicked += OnBuyButtonClicked;

            IReadOnlyVariable<int> statLevel = _statUpgradeService.GetStatLevelFor(_statType);
            _disposables.Add(statLevel.Subscribe(OnStatUpgradeLevelChanged));

            IReadOnlyVariable<int> currency = _walletService.GetCurrency(_statUpgradeService.GetUpgradeCostTypeFor(_statType));
            _disposables.Add(currency.Subscribe(OnWalletChanged));
        }

        private void OnStatUpgradeLevelChanged(int arg1, int arg2) => _view.SetStatValueText(GetStatValueText());
        private void OnWalletChanged(int arg1, int arg2) => UpdateBuyButtonState();

        public void Dispose()
        {
            _view.BuyButtonView.Clicked -= OnBuyButtonClicked;

            foreach (IDisposable disposable in _disposables)
                disposable.Dispose();
        }

        private void OnBuyButtonClicked()
        {
            if (_statUpgradeService.TryGetUpgradeCostFor(_statType, out CurrencyType currencyType, out int cost))
            {
                if (_walletService.Enough(currencyType, cost))
                {
                    if (_statUpgradeService.TryUpgradeStat(_statType) == false)
                        throw new Exception();

                    _walletService.Spend(currencyType, cost);
                }
                else
                {
                    Debug.Log("Not enough currency");
                }
            }
            else
            {
                Debug.Log("Already max");
            }
        }

        private void UpdateBuyButtonState()
        {
            if (_statUpgradeService.TryGetUpgradeCostFor(_statType, out CurrencyType currencyType, out int cost))
            {
                _view.BuyButtonView.SetPriceText(cost.ToString());
                _view.BuyButtonView.ShowIcon();
                _view.BuyButtonView.SetPriceIcon(_currencyIconsConfig.GetSpriteFor(currencyType));

                if (_walletService.Enough(currencyType, cost))
                    _view.BuyButtonView.Unlock();
                else
                    _view.BuyButtonView.Lock();
            }
            else
            {
                _view.BuyButtonView.HideIcon();
                _view.BuyButtonView.Lock();
                _view.BuyButtonView.SetPriceText("MAX");
            }
        }

        private string GetStatValueText()
        {
            float statValue = _statUpgradeService.GetCurrentStatValueFor(_statType);
            string result = statValue.ToString(CultureInfo.InvariantCulture);

            if (_statUpgradeService.TryGetStatValueForNextLevel(_statType, out float nextStatValue))
                result += $"<color=green>>{nextStatValue}</color>";

            return result;
        }
    }
}
