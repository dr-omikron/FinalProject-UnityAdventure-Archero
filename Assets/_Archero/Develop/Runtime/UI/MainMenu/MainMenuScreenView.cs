using System;
using _Archero.Develop.Runtime.UI.CommonViews;
using _Archero.Develop.Runtime.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace _Archero.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        public event Action OpenLevelsMenuButtonClicked;

        [field:SerializeField] public IconTextListView WalletView { get; private set; }
        [SerializeField] private Button _openLevelsMenuButton;

        private void OnEnable()
        {
            _openLevelsMenuButton.onClick.AddListener(OnOpenLevelsMenuButtonClicked);
        }

        private void OnDisable()
        {
            _openLevelsMenuButton.onClick.RemoveListener(OnOpenLevelsMenuButtonClicked);
        }

        private void OnOpenLevelsMenuButtonClicked() => OpenLevelsMenuButtonClicked?.Invoke();
    }
}
