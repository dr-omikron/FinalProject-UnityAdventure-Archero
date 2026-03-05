using System.Collections.Generic;
using _Archero.Develop.Runtime.UI.Core;
using _Archero.Develop.Runtime.UI.Gameplay.HealthDisplay;
using _Archero.Develop.Runtime.UI.Gameplay.Stages;

namespace _Archero.Develop.Runtime.UI.Gameplay
{
    public class GameplayScreenPresenter : IPresenter
    {
        private readonly GameplayScreenView _screen;
        private readonly GameplayPresentersFactory _presentersFactory;
        private readonly List<IPresenter> _childPresenters = new List<IPresenter>();
        private EntityHealthDisplayPresenter _entityHealthDisplayPresenter;

        public GameplayScreenPresenter(GameplayScreenView screen, GameplayPresentersFactory presentersFactory)
        {
            _screen = screen;
            _presentersFactory = presentersFactory;
        }

        public void Initialize()
        {
            CreateStageNumber();
            CreateEntityHealthDisplay();

            foreach (var childPresenter in _childPresenters)
                childPresenter.Initialize();
        }

        public void Dispose()
        {
            foreach (var childPresenter in _childPresenters)
                childPresenter.Dispose();

            _childPresenters.Clear();
        }

        public void LateUpdate()
        {
            _entityHealthDisplayPresenter.LateUpdate();
        }

        private void CreateStageNumber()
        {
            StagePresenter stagePresenter = _presentersFactory.CreateStagePresenter(_screen.StageNumberView);
            _childPresenters.Add(stagePresenter);
        }

        private void CreateEntityHealthDisplay()
        {
            _entityHealthDisplayPresenter =
                _presentersFactory.CreateEntityHealthDisplayPresenter(_screen.EntitiesHealthDisplay);

            _childPresenters.Add(_entityHealthDisplayPresenter);
        }
    }
}
