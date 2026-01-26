using _Archero.Develop.Runtime.Infrastructure.DI;

namespace _Archero.Develop.Runtime.UI.MainMenu
{
    public class MainMenuPresenterFactory
    {
        private readonly DIContainer _container;

        public MainMenuPresenterFactory(DIContainer container)
        {
            _container = container;
        }

        public MainMenuScreenPresenter CreateMainMenuScreen(MainMenuScreenView view)
        {
            return new MainMenuScreenPresenter(
                view,
                _container.Resolve<ProjectPresenterFactory>(),
                _container.Resolve<MainMenuPopupService>());
        }

    }
}
