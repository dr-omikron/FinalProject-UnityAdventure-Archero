using _Archero.Develop.Runtime.Configs.Meta.Wallet;
using _Archero.Develop.Runtime.Infrastructure.DI;
using _Archero.Develop.Runtime.Meta.Features.LevelsProgression;
using _Archero.Develop.Runtime.Meta.Features.Wallet;
using _Archero.Develop.Runtime.UI.CommonViews;
using _Archero.Develop.Runtime.UI.Core;
using _Archero.Develop.Runtime.UI.Core.TestPopup;
using _Archero.Develop.Runtime.UI.LevelMenuPopup;
using _Archero.Develop.Runtime.UI.Wallet;
using _Archero.Develop.Runtime.Utilities.ConfigsManagement;
using _Archero.Develop.Runtime.Utilities.CoroutinesManagement;
using _Archero.Develop.Runtime.Utilities.Reactive;
using _Archero.Develop.Runtime.Utilities.SceneManagement;

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

        public TestPopupPresenter CreateTestPopupPresenter(TestPopupView view)
        {
            return new TestPopupPresenter(view, _container.Resolve<ICoroutinesPerformer>());
        }

        public LevelTilePresenter CreateLevelTilePresenter(LevelTileView view, int levelNumber)
        {
            return new LevelTilePresenter(
                _container.Resolve<LevelsProgressionService>(),
                _container.Resolve<SceneSwitcherService>(),
                _container.Resolve<ICoroutinesPerformer>(),
                levelNumber,
                view);
        }

        public LevelsMenuPopupPresenter CreateLevelsMenuPresenter(LevelsMenuPopupView view)
        {
            return new LevelsMenuPopupPresenter(
                _container.Resolve<ICoroutinesPerformer>(),
                _container.Resolve<ConfigsProviderService>(),
                this,
                _container.Resolve<ViewsFactory>(),
                view);
        }
    }
}
