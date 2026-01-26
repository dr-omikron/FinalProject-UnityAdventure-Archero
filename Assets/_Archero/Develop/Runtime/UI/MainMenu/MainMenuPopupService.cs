using _Archero.Develop.Runtime.UI.Core;
using UnityEngine;

namespace _Archero.Develop.Runtime.UI.MainMenu
{
    public class MainMenuPopupService : PopupService
    {
        private readonly MainMenuUIRoot _uiRoot;

        public MainMenuPopupService(
            ViewsFactory viewsFactory, 
            ProjectPresenterFactory presentersFactory, MainMenuUIRoot uiRoot) 
            : base(viewsFactory, presentersFactory)
        {
            _uiRoot = uiRoot;
        }

        protected override Transform PopupLayer => _uiRoot.PupUpsLayer;
    }
}
