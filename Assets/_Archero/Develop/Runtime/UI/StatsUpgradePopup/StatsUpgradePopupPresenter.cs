using System.Collections.Generic;
using _Archero.Develop.Runtime.Gameplay.Features.StatsFeature;
using _Archero.Develop.Runtime.Meta.Features.StatsUpgrade;
using _Archero.Develop.Runtime.UI.Core;
using _Archero.Develop.Runtime.UI.Wallet;
using _Archero.Develop.Runtime.Utilities.CoroutinesManagement;

namespace _Archero.Develop.Runtime.UI.StatsUpgradePopup
{
    public class StatsUpgradePopupPresenter : PopupPresenterBase
    {
        private readonly StatsUpgradePopupView _view;
        private readonly ViewsFactory _viewFactory;
        private readonly ProjectPresenterFactory _projectPresenterFactory;
        private readonly StatsUpgradeService _statsUpgradeService;

        private readonly List<UpgradableStatPresenter> _upgradableStatPresenters = new List<UpgradableStatPresenter>();
        private WalletPresenter _walletPresenter;
        private CharacterPreviewPresenter _characterPreviewPresenter;

        public StatsUpgradePopupPresenter(
            ICoroutinesPerformer coroutinesPerformer, 
            StatsUpgradePopupView view, 
            ProjectPresenterFactory projectPresenterFactory, 
            StatsUpgradeService statsUpgradeService, 
            ViewsFactory viewFactory) : base(coroutinesPerformer)
        {
            _view = view;
            _projectPresenterFactory = projectPresenterFactory;
            _statsUpgradeService = statsUpgradeService;
            _viewFactory = viewFactory;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();

            _view.SetTitle("UPGRADE YOUR STATS");

            _walletPresenter = _projectPresenterFactory.CreateWalletPresenter(_view.CurrencyListView);
            _walletPresenter.Initialize();

            _characterPreviewPresenter = _projectPresenterFactory.CreateCharacterPreviewPresenter();
            _characterPreviewPresenter.Initialize();

            foreach (StatTypes statType in _statsUpgradeService.AvailableStats())
            {
                UpgradableStatView upgradableStatView = _viewFactory.Create<UpgradableStatView>(ViewIDs.UpgradableStatView);
                _view.UpgradableStatListView.Add(upgradableStatView);

                UpgradableStatPresenter upgradableStatPresenter = _projectPresenterFactory.CreateUpgradableStatPresenter(upgradableStatView, statType);
                _upgradableStatPresenters.Add(upgradableStatPresenter);
                upgradableStatPresenter.Initialize();
            }
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();

            foreach (UpgradableStatPresenter presenter in _upgradableStatPresenters)
                presenter.Dispose();
        }

        public override void Dispose()
        {
            base.Dispose();

            foreach (UpgradableStatPresenter presenter in _upgradableStatPresenters)
            {
                presenter.Dispose();
                _view.UpgradableStatListView.Remove(presenter.View);
                _viewFactory.Release(presenter.View);
            }

            _upgradableStatPresenters.Clear();
            _walletPresenter.Dispose();
            _characterPreviewPresenter.Dispose();
        }
    }
}
