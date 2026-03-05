using _Archero.Develop.Runtime.UI.CommonViews;
using _Archero.Develop.Runtime.UI.Core;
using _Archero.Develop.Runtime.UI.Gameplay.HealthDisplay;
using UnityEngine;

namespace _Archero.Develop.Runtime.UI.Gameplay
{
    public class GameplayScreenView : MonoBehaviour, IView
    {
        [field: SerializeField] public IconTextView StageNumberView { get; private set; }
        [field: SerializeField] public EntitiesHealthDisplay EntitiesHealthDisplay { get; private set; }
    }
}
