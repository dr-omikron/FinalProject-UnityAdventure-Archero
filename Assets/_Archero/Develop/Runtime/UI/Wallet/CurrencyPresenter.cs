using System;
using _Archero.Develop.Runtime.Configs.Meta.Wallet;
using _Archero.Develop.Runtime.Meta.Features.Wallet;
using _Archero.Develop.Runtime.UI.CommonViews;
using _Archero.Develop.Runtime.UI.Core;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.UI.Wallet
{
    public class CurrencyPresenter : IPresenter
    {
        private readonly IReadOnlyVariable<int> _currency;
        private readonly CurrencyType _currencyType;
        private readonly CurrencyIconsConfig _currencyIconsConfig;

        private readonly IconTextView _view;

        private IDisposable _subscription;

        public CurrencyPresenter(IReadOnlyVariable<int> currency, CurrencyType currencyType, CurrencyIconsConfig currencyIconsConfig, IconTextView view)
        {
            _currency = currency;
            _currencyType = currencyType;
            _currencyIconsConfig = currencyIconsConfig;
            _view = view;
        }

        public IconTextView View => _view;

        public void Initialize()
        {
            UpdateValue(_currency.Value);
            _view.SetIcon(_currencyIconsConfig.GetSpriteFor(_currencyType));

            _subscription = _currency.Subscribe(OnCurrencyChanged);
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }

        private void OnCurrencyChanged(int oldValue, int newValue) => UpdateValue(newValue);

        private void UpdateValue(int value) => _view.SetText(value.ToString());
    }
}
