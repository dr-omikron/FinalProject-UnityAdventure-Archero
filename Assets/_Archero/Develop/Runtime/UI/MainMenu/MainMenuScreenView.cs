using _Archero.Develop.Runtime.UI.CommonViews;
using _Archero.Develop.Runtime.UI.Core;
using UnityEngine;

namespace _Archero.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        [field:SerializeField] public IconTextListView WalletView { get; private set; }
    }
}
