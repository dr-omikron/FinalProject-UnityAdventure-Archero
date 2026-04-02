using System;
using System.Linq;
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

        private readonly int _level;

        public SelectableAbilityPresenter(
            AbilityConfig abilityConfig, 
            SelectableAbilityView view, 
            AbilityFactory abilityFactory, 
            Entity entity, int level)
        {
            AbilityConfig = abilityConfig;
            View = view;
            _abilityFactory = abilityFactory;
            _entity = entity;
            _level = level;
        }

        public AbilityConfig AbilityConfig { get; }
        public SelectableAbilityView View { get; }

        public void Initialize()
        {
            View.SetName(AbilityConfig.Name);
            View.SetDescription(AbilityConfig.Description);
            View.AbilityIcon.SetIcon(AbilityConfig.Icon);

            InitByAbilityConfig();

            View.Clicked += OnViewClicked;
        }

        public void Dispose() => View.Clicked -= OnViewClicked;

        public void Provide()
        {
            Ability ability;

            if (AbilityConfig.IsUpgradable())
            {
                ability = _entity.Abilities.Elements.FirstOrDefault(ability => ability.ID == AbilityConfig.ID);

                if (ability != null)
                {
                    ability.AddLevel(_level);
                    return;
                }
            }

            ability = _abilityFactory.CreateAbilityFor(_entity, AbilityConfig, _level);
            _entity.Abilities.Add(ability);
        }

        private void InitByAbilityConfig()
        {
            if (AbilityConfig.IsUpgradable())
            {
                Ability ability = _entity.Abilities.Elements.FirstOrDefault(ability => ability.ID == AbilityConfig.ID);

                if (ability != null)
                {
                    View.AbilityIcon.ShowLevel();
                    View.AbilityIcon.SetLevel("LV." + ability.CurrentLevel.Value);
                    View.SetTablet("LV." + ability.CurrentLevel.Value + "->" + "LV." + (ability.CurrentLevel.Value + _level));
                }
                else
                {
                    View.AbilityIcon.HideLevel();
                    View.SetTablet("NEW LV." + _level);
                }
            }
            else
            {
                View.AbilityIcon.HideLevel();
                View.SetTablet("NEW");
            }
        }

        private void OnViewClicked() => Selected?.Invoke(this);
    }
}
