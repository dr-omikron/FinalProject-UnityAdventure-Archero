using _Archero.Develop.Runtime.UI.CommonViews;
using UnityEngine;

namespace _Archero.Develop.Runtime.UI.Gameplay.HealthDisplay
{
    public class EntitiesHealthDisplay : ElementsListView<BarWithText>
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void UpdatePositionFor(BarWithText bar, Vector3 worldPosition)
        {
            Vector3 screenPosition = _camera.WorldToScreenPoint(worldPosition);
            bar.transform.position = screenPosition;
        }
    }
}
