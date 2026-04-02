using System;
using _Archero.Develop.Runtime.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Archero.Develop.Runtime.UI.StatsUpgradePopup
{
    public class BuyButtonView : MonoBehaviour, IView
    {
        public event Action Clicked;
        
        [SerializeField] private Button _button;
        [SerializeField] private Image _background;
        [SerializeField] private Sprite _availableSprite;
        [SerializeField] private Sprite _lockedSprite;
        [SerializeField] private Image _priceIcon;
        [Space, SerializeField] private TMP_Text _priceText;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        protected virtual void OnClicked()
        {
            Clicked?.Invoke();
        }
        
        public virtual void Lock() => _background.sprite = _lockedSprite;
        public virtual void Unlock() => _background.sprite = _availableSprite;

        public void SetPriceText(string price) => _priceText.text = price;
        public void SetPriceIcon(Sprite sprite) => _priceIcon.sprite = sprite;

        public void HideIcon() => _priceIcon.gameObject.SetActive(false);
        public void ShowIcon() => _priceIcon.gameObject.SetActive(true);
    }
}
