using System;
using _Archero.Develop.Runtime.UI.CommonViews;
using _Archero.Develop.Runtime.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace _Archero.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        public event Action OpenTestPopupButtonClicked;

        [field:SerializeField] public IconTextListView WalletView { get; private set; }
        [SerializeField] private Button _openTestPopupButton;

        private void OnEnable()
        {
            _openTestPopupButton.onClick.AddListener(OnOpenTestPopupButtonClicked);
        }

        private void OnDisable()
        {
            _openTestPopupButton.onClick.RemoveListener(OnOpenTestPopupButtonClicked);
        }

        private void OnOpenTestPopupButtonClicked() => OpenTestPopupButtonClicked?.Invoke();
    }
}
