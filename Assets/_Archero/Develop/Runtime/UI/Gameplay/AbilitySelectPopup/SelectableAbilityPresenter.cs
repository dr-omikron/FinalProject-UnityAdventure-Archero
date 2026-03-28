using System;
using _Archero.Develop.Runtime.Configs.Abilities;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.Features.AbilitiesFeature;
using _Archero.Develop.Runtime.UI.Core;

namespace _Archero.Develop.Runtime.UI.Gameplay.AbilitySelectPopup
{
    public class SelectableAbilityPresenter : IPresenter
    {
        public event Action<SelectableAbilityPresenter> Selected;

        private readonly AbilityFactory _abilityFactory;
        private readonly Entity _entity;

        public SelectableAbilityPresenter(
            AbilityConfig abilityConfig, 
            SelectableAbilityView view, 
            AbilityFactory abilityFactory, 
            Entity entity)
        {
            AbilityConfig = abilityConfig;
            View = view;
            _abilityFactory = abilityFactory;
            _entity = entity;
        }

        public AbilityConfig AbilityConfig { get; }
        public SelectableAbilityView View { get; }

        public void Initialize()
        {
            View.SetName(AbilityConfig.Name);
            View.SetDescription(AbilityConfig.Description);
            View.AbilityIcon.SetIcon(AbilityConfig.Icon);
            View.AbilityIcon.HideLevel();
            View.SetTablet("NEW");
            View.Clicked += OnViewClicked;
        }

        public void Dispose() => View.Clicked -= OnViewClicked;

        public void Provide()
        {
            Ability ability = _abilityFactory.CreateAbilityFor(_entity, AbilityConfig);
            _entity.Abilities.Add(ability);
        }

        private void OnViewClicked() => Selected?.Invoke(this);
    }
}
