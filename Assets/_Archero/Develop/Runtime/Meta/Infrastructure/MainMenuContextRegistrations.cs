using _Archero.Develop.Runtime.Infrastructure.DI;
using _Archero.Develop.Runtime.UI;
using _Archero.Develop.Runtime.UI.Core;
using _Archero.Develop.Runtime.UI.MainMenu;
using _Archero.Develop.Runtime.Utilities.AssetsManagement;
using UnityEngine;

namespace _Archero.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateMainMenuUIRoot).NonLazy();
            container.RegisterAsSingle(CreateMainMenuPresenterFactory);
            container.RegisterAsSingle(CreateMainMenuPopupService);
            container.RegisterAsSingle(CreateMainMenuScreenPresenter).NonLazy();
        }

        private static MainMenuUIRoot CreateMainMenuUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            MainMenuUIRoot mainMenuUIRoot = 
                resourcesAssetsLoader.Load<MainMenuUIRoot>("UI/MainMenu/MainMenuUIRoot");

            return Object.Instantiate(mainMenuUIRoot);
        }

        private static MainMenuPresenterFactory CreateMainMenuPresenterFactory(DIContainer c) 
            => new MainMenuPresenterFactory(c);

        private static MainMenuScreenPresenter CreateMainMenuScreenPresenter(DIContainer c)
        {
            MainMenuUIRoot uiRoot = c.Resolve<MainMenuUIRoot>();
            MainMenuScreenView view = c
                .Resolve<ViewsFactory>()
                .Create<MainMenuScreenView>(ViewIDs.MainMenuScreen, uiRoot.HUDLayer);

            return c.Resolve<MainMenuPresenterFactory>().CreateMainMenuScreen(view);
        }

        private static MainMenuPopupService CreateMainMenuPopupService(DIContainer c)
        {
            return new MainMenuPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<ProjectPresenterFactory>(),
                c.Resolve<MainMenuUIRoot>());
        }
    }
}
