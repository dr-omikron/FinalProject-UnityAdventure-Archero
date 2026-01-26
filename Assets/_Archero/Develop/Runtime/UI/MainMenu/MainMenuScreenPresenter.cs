using System.Collections.Generic;
using _Archero.Develop.Runtime.UI.Core;
using _Archero.Develop.Runtime.UI.Wallet;

namespace _Archero.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenPresenter : IPresenter
    {
        private readonly MainMenuScreenView _mainMenuScreenView;
        private readonly ProjectPresenterFactory _projectPresenterFactory;
        
        private readonly List<IPresenter> _childPresenters = new List<IPresenter>();

        public MainMenuScreenPresenter(MainMenuScreenView mainMenuScreenView, ProjectPresenterFactory projectPresenterFactory)
        {
            _mainMenuScreenView = mainMenuScreenView;
            _projectPresenterFactory = projectPresenterFactory;
        }

        public void Initialize()
        {
            CreateWallet();

            foreach (IPresenter childPresenter in _childPresenters)
                childPresenter.Initialize();
        }

        public void Dispose()
        {
            foreach (IPresenter childPresenter in _childPresenters)
                childPresenter.Dispose();

            _childPresenters.Clear();
        }

        private void CreateWallet()
        {
            WalletPresenter walletPresenter = _projectPresenterFactory.CreateWalletPresenter(_mainMenuScreenView.WalletView);
            _childPresenters.Add(walletPresenter);
        }
    }
}
