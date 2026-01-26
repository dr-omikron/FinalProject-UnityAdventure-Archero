using _Archero.Develop.Runtime.Configs.Meta.Wallet;
using _Archero.Develop.Runtime.Infrastructure.DI;
using _Archero.Develop.Runtime.Meta.Features.Wallet;
using _Archero.Develop.Runtime.UI.CommonViews;
using _Archero.Develop.Runtime.UI.Core;
using _Archero.Develop.Runtime.UI.Wallet;
using _Archero.Develop.Runtime.Utilities.ConfigsManagement;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.UI
{
    public class ProjectPresenterFactory
    {
        private readonly DIContainer _container;

        public ProjectPresenterFactory(DIContainer container)
        {
            _container = container;
        }

        public CurrencyPresenter CreateCurrencyPresenter(
            IconTextView view, 
            IReadOnlyVariable<int> currency,
            CurrencyType currencyType)
        {
            return new CurrencyPresenter(
                currency, 
                currencyType,
                _container.Resolve<ConfigsProviderService>().GetConfig<CurrencyIconsConfig>(), 
                view);
        }

        public WalletPresenter CreateWalletPresenter(IconTextListView views)
        {
            return new WalletPresenter(
                _container.Resolve<WalletService>(),
                this,
                _container.Resolve<ViewsFactory>(),
                views);
        }
    }
}
