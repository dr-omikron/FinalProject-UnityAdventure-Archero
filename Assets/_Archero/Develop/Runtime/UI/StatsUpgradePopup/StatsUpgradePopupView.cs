using _Archero.Develop.Runtime.UI.CommonViews;
using _Archero.Develop.Runtime.UI.Core;
using TMPro;
using UnityEngine;

namespace _Archero.Develop.Runtime.UI.StatsUpgradePopup
{
    public class StatsUpgradePopupView : PopupViewBase
    {
        [SerializeField] private TMP_Text _title;
        [field: SerializeField] public IconTextListView CurrencyListView { get; private set; }
        [field: SerializeField] public UpgradableStatListView UpgradableStatListView { get; private set; }

        public void SetTitle(string title) => _title.text = title;
    }
}
