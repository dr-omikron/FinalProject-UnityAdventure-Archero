using System.Collections.Generic;
using _Archero.Develop.Runtime.Configs.Abilities;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.Features.AbilitiesDroppingFeature;
using _Archero.Develop.Runtime.UI.Core;
using _Archero.Develop.Runtime.Utilities.CoroutinesManagement;

namespace _Archero.Develop.Runtime.UI.Gameplay.AbilitySelectPopup
{
    public class AbilitySelectPopupPresenter : PopupPresenterBase
    {
        private const int AbilitiesCount = 3;
        
        private const string Title = "LEVEL {0} IN THIS ADVENTURE";
        private const string SelectAbilityText = "Select Ability";
        
        private readonly AbilitySelectPopupView _view;
        private readonly Entity _entity;
        private readonly AbilityDropService _abilityDropService;
        private readonly GameplayPresentersFactory _gameplayPresentersFactory;
        private readonly ViewsFactory _viewsFactory;
        private readonly int _level;
        
        private readonly List<SelectableAbilityPresenter> _abilityPresenters = new List<SelectableAbilityPresenter>();
        private SelectableAbilityPresenter _selectedPresenter;

        public AbilitySelectPopupPresenter(
            ICoroutinesPerformer coroutinesPerformer, 
            AbilitySelectPopupView view, 
            Entity entity, 
            AbilityDropService abilityDropService, 
            GameplayPresentersFactory gameplayPresentersFactory, 
            ViewsFactory viewsFactory, int level) : base(coroutinesPerformer)
        {
            _view = view;
            _entity = entity;
            _abilityDropService = abilityDropService;
            _gameplayPresentersFactory = gameplayPresentersFactory;
            _viewsFactory = viewsFactory;
            _level = level;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();
            
            _view.SetTitle(string.Format(Title, _level));
            _view.SetAdditionalText(SelectAbilityText);
            _view.SelectButtonOff();

            _view.SelectButtonClicked += OnSelectButtonClicked;

            List<AbilityConfig> dropOptions = _abilityDropService.Drop(AbilitiesCount, _entity);

            for (int i = 0; i < dropOptions.Count; i++)
            {
                SelectableAbilityView selectableAbilityView = _viewsFactory.Create<SelectableAbilityView>(ViewIDs.SelectableAbilityView);
                _view.AbilityListView.Add(selectableAbilityView);

                SelectableAbilityPresenter presenter = _gameplayPresentersFactory
                    .CreateSelectableAbilityPresenter(dropOptions[i], selectableAbilityView, _entity);

                presenter.Selected += OnPresenterSelected;
                presenter.Initialize();

                _abilityPresenters.Add(presenter);
            }
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();

            _view.SelectButtonOff();
            _view.SelectButtonClicked -= OnSelectButtonClicked;

            foreach (SelectableAbilityPresenter presenter in _abilityPresenters)
                presenter.Selected -= OnPresenterSelected;
        }

        public override void Dispose()
        {
            base.Dispose();

            _view.SelectButtonClicked -= OnSelectButtonClicked;

            foreach (SelectableAbilityPresenter presenter in _abilityPresenters)
            {
                presenter.Selected -= OnPresenterSelected;
                _view.AbilityListView.Remove(presenter.View);
                _viewsFactory.Release(presenter.View);
                presenter.Dispose();
            }

            _abilityPresenters.Clear();
        }

        private void OnPresenterSelected(SelectableAbilityPresenter selected)
        {
            _view.SelectButtonOn();
            _view.AbilityListView.Select(selected.View);
            _selectedPresenter = selected;
        }

        private void OnSelectButtonClicked()
        {
            _selectedPresenter.Provide();
            OnCloseRequest();
        }
    }
}
