using _Archero.Develop.Runtime.Configs.Abilities;
using _Archero.Develop.Runtime.Configs.Gameplay;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.Features.AbilitiesDroppingFeature;
using _Archero.Develop.Runtime.Gameplay.Features.AbilitiesFeature;
using _Archero.Develop.Runtime.Gameplay.Features.MainHero;
using _Archero.Develop.Runtime.Gameplay.Features.StagesFeature;
using _Archero.Develop.Runtime.Gameplay.Infrastructure;
using _Archero.Develop.Runtime.Infrastructure.DI;
using _Archero.Develop.Runtime.UI.CommonViews;
using _Archero.Develop.Runtime.UI.Core;
using _Archero.Develop.Runtime.UI.Gameplay.AbilitySelectPopup;
using _Archero.Develop.Runtime.UI.Gameplay.Experience;
using _Archero.Develop.Runtime.UI.Gameplay.HealthDisplay;
using _Archero.Develop.Runtime.UI.Gameplay.ResultPopups;
using _Archero.Develop.Runtime.UI.Gameplay.Stages;
using _Archero.Develop.Runtime.Utilities.ConfigsManagement;
using _Archero.Develop.Runtime.Utilities.CoroutinesManagement;
using _Archero.Develop.Runtime.Utilities.SceneManagement;

namespace _Archero.Develop.Runtime.UI.Gameplay
{
    public class GameplayPresentersFactory
    {
        private readonly DIContainer _container;
        private readonly GameplayInputArgs _gameplayInputArgs;

        public GameplayPresentersFactory(DIContainer container, GameplayInputArgs gameplayInputArgs)
        {
            _container = container;
            _gameplayInputArgs = gameplayInputArgs;
        }

        public GameplayScreenPresenter CreateGameplayScreenPresenter(GameplayScreenView view)
        {
            return new GameplayScreenPresenter(view, _container.Resolve<GameplayPresentersFactory>());
        }

        public WinPopupPresenter CreateWinPopupPresenter(WinPopupView view)
        {
            return new WinPopupPresenter(
                _container.Resolve<ICoroutinesPerformer>(),
                view,
                _container.Resolve<SceneSwitcherService>());
        }

        public DefeatPopupPresenter CreateDefeatPopupPresenter(DefeatPopupView view)
        {
            return new DefeatPopupPresenter(
                _container.Resolve<ICoroutinesPerformer>(),
                view,
                _container.Resolve<SceneSwitcherService>(),
                _gameplayInputArgs);
        }

        public StagePresenter CreateStagePresenter(IconTextView view)
        {
            return new StagePresenter(view,_container.Resolve<StageProviderService>());
        }

        public EntityHealthPresenter CreateEntityHealthPresenter(Entity entity, BarWithText view)
        {
            return new EntityHealthPresenter(entity, view);
        }

        public EntityHealthDisplayPresenter CreateEntityHealthDisplayPresenter(EntitiesHealthDisplay view)
        {
            return new EntityHealthDisplayPresenter(
                _container.Resolve<EntitiesLifeContext>(),
                view,
                this,
                _container.Resolve<ViewsFactory>());
        }

        public SelectableAbilityPresenter CreateSelectableAbilityPresenter(
            AbilityConfig abilityConfig,
            SelectableAbilityView view,
            Entity entity)
        {
            return new SelectableAbilityPresenter(abilityConfig, view, _container.Resolve<AbilityFactory>(), entity);
        }

        public AbilitySelectPopupPresenter CreateAbilitySelectPopupPresenter(AbilitySelectPopupView view, Entity entity, int level)
        {
            return new AbilitySelectPopupPresenter(
                _container.Resolve<ICoroutinesPerformer>(),
                view,
                entity,
                _container.Resolve<AbilityDropService>(),
                this,
                _container.Resolve<ViewsFactory>(), 
                level);
        }

        public MainHeroExperiencePresenter CreateMainHeroExperiencePresenter(BarWithText view)
        {
            return new MainHeroExperiencePresenter(
                _container.Resolve<MainHeroHolderService>(),
                view,
                _container.Resolve<ConfigsProviderService>().GetConfig<ExperienceForUpgradeLevelConfig>());
        }
    }
}
