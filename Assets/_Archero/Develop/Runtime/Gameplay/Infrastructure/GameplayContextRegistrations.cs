using _Archero.Develop.Runtime.Configs.Abilities;
using _Archero.Develop.Runtime.Configs.Gameplay.Levels;
using _Archero.Develop.Runtime.Configs.Loot;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Archero.Develop.Runtime.Gameplay.Features.AbilitiesDroppingFeature;
using _Archero.Develop.Runtime.Gameplay.Features.AbilitiesFeature;
using _Archero.Develop.Runtime.Gameplay.Features.AI;
using _Archero.Develop.Runtime.Gameplay.Features.Enemies;
using _Archero.Develop.Runtime.Gameplay.Features.InputFeatures;
using _Archero.Develop.Runtime.Gameplay.Features.LevelUpFeature;
using _Archero.Develop.Runtime.Gameplay.Features.LootFeature;
using _Archero.Develop.Runtime.Gameplay.Features.MainHero;
using _Archero.Develop.Runtime.Gameplay.Features.PauseFeature;
using _Archero.Develop.Runtime.Gameplay.Features.StagesFeature;
using _Archero.Develop.Runtime.Gameplay.States;
using _Archero.Develop.Runtime.Infrastructure.DI;
using _Archero.Develop.Runtime.UI;
using _Archero.Develop.Runtime.UI.Core;
using _Archero.Develop.Runtime.UI.Gameplay;
using _Archero.Develop.Runtime.Utilities.AssetsManagement;
using _Archero.Develop.Runtime.Utilities.ConfigsManagement;
using _Archero.Develop.Runtime.Utilities.CoroutinesManagement;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        private static GameplayInputArgs _inputArgs;

        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            _inputArgs = args;

            container.RegisterAsSingle(CreateEntitiesFactory);
            container.RegisterAsSingle(CreateEntitiesLifeContext);
            container.RegisterAsSingle(CreateMonoEntityFactory).NonLazy();
            container.RegisterAsSingle(CreateColliderRegistryService);
            container.RegisterAsSingle(CreateBrainsFactory);
            container.RegisterAsSingle(CreateAIBrainContext);
            container.RegisterAsSingle<IInputService>(CreateDesktopInput);
            container.RegisterAsSingle(CreateMainHeroFactory);
            container.RegisterAsSingle(CreateEnemiesFactory);
            container.RegisterAsSingle(CreateStagesFactory);
            container.RegisterAsSingle(CreateStageProviderService);
            container.RegisterAsSingle(CreatePreparationTriggerService);
            container.RegisterAsSingle(CreateMainHeroHolderService).NonLazy();
            container.RegisterAsSingle(CreateGameplayStatesFactory);
            container.RegisterAsSingle(CreateGameplayStatesContext);
            container.RegisterAsSingle(CreateGameplayPresentersFactory);
            container.RegisterAsSingle(CreateGameplayScreenPresenter).NonLazy();
            container.RegisterAsSingle(CreateGameplayUIRoot).NonLazy();
            container.RegisterAsSingle(CreateGameplayPopupService);
            container.RegisterAsSingle(CreateAbilityFactory);
            container.RegisterAsSingle(CreateAbilityDropService);
            container.RegisterAsSingle(CreateAbilitiesDroppingRulesService);
            container.RegisterAsSingle(CreateDropAbilityOnMainHeroLevelUpService).NonLazy();
            container.RegisterAsSingle<IPauseService>(CreateTimeScalePauseService);
            container.RegisterAsSingle(CreateLootFactory);
            container.RegisterAsSingle(CreateDropLootService);
            container.RegisterAsSingle(CreateLootPullingService).NonLazy();
        }

        private static EntitiesLifeContext CreateEntitiesLifeContext(DIContainer c)
            => new EntitiesLifeContext();

        private static EntitiesFactory CreateEntitiesFactory(DIContainer c)
            => new EntitiesFactory(c);

        private static MonoEntityFactory CreateMonoEntityFactory(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();
            EntitiesLifeContext entitiesLifeContext = c.Resolve<EntitiesLifeContext>();
            ColliderRegistryService colliderRegistryService = c.Resolve<ColliderRegistryService>();

            return new MonoEntityFactory(resourcesAssetsLoader, entitiesLifeContext, colliderRegistryService);
        }
        
        private static ColliderRegistryService CreateColliderRegistryService(DIContainer c)
            => new ColliderRegistryService();

        private static BrainsFactory CreateBrainsFactory(DIContainer c) => new BrainsFactory(c);

        private static AIBrainContext CreateAIBrainContext(DIContainer c) => new AIBrainContext();

        private static DesktopInput CreateDesktopInput(DIContainer c) => new DesktopInput();

        private static MainHeroFactory CreateMainHeroFactory(DIContainer c) => new MainHeroFactory(c);

        private static EnemiesFactory CreateEnemiesFactory(DIContainer c) => new EnemiesFactory(c);
        
        private static StagesFactory CreateStagesFactory(DIContainer c) => new StagesFactory(c);

        private static StageProviderService CreateStageProviderService(DIContainer c)
        {
            return new StageProviderService(
                c.Resolve<ConfigsProviderService>().GetConfig<LevelsListConfig>().GetBy(_inputArgs.LevelNumber),
                c.Resolve<StagesFactory>());
        }

        private static PreparationTriggerService CreatePreparationTriggerService(DIContainer c)
        {
            return new PreparationTriggerService(
                c.Resolve<EntitiesFactory>(),
                c.Resolve<EntitiesLifeContext>());
        }

        private static MainHeroHolderService CreateMainHeroHolderService(DIContainer c)
            => new MainHeroHolderService(c.Resolve<EntitiesLifeContext>());

        private static GameplayStatesFactory CreateGameplayStatesFactory(DIContainer c)
            => new GameplayStatesFactory(c);

        private static GameplayStatesContext CreateGameplayStatesContext(DIContainer c)
            => new GameplayStatesContext(c.Resolve<GameplayStatesFactory>().CreateGameplayStateMachine(_inputArgs));
        
        private static GameplayPresentersFactory CreateGameplayPresentersFactory(DIContainer c)
            => new GameplayPresentersFactory(c, _inputArgs);

        private static GameplayScreenPresenter CreateGameplayScreenPresenter(DIContainer c)
        {
            GameplayUIRoot uiRoot = c.Resolve<GameplayUIRoot>();

            GameplayScreenView view = c
                .Resolve<ViewsFactory>()
                .Create<GameplayScreenView>(ViewIDs.GameplayScreen, uiRoot.HUDLayer);

            GameplayScreenPresenter presenter = c
                .Resolve<GameplayPresentersFactory>()
                .CreateGameplayScreenPresenter(view);

            return presenter;
        }

        private static GameplayUIRoot CreateGameplayUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();
            
            GameplayUIRoot gameplayUIRootPrefab = resourcesAssetsLoader
                .Load<GameplayUIRoot>("UI/Gameplay/GameplayUIRoot");

            return Object.Instantiate(gameplayUIRootPrefab);
        }

        private static GameplayPopupService CreateGameplayPopupService(DIContainer c)
        {
            return new GameplayPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<ProjectPresenterFactory>(),
                c.Resolve<GameplayUIRoot>(),
                c.Resolve<GameplayPresentersFactory>());
        }

        private static AbilityFactory CreateAbilityFactory(DIContainer c)
            => new AbilityFactory(c);

        private static AbilityDropService CreateAbilityDropService(DIContainer c)
        {
            return new AbilityDropService(
                c.Resolve<ConfigsProviderService>().GetConfig<AbilitiesConfigsContainer>(),
                c.Resolve<AbilitiesDroppingRulesService>());
        }

        private static AbilitiesDroppingRulesService CreateAbilitiesDroppingRulesService(DIContainer c) 
            => new AbilitiesDroppingRulesService();

        private static DropAbilityOnMainHeroLevelUpService CreateDropAbilityOnMainHeroLevelUpService(DIContainer c)
        {
            return new DropAbilityOnMainHeroLevelUpService(
                c.Resolve<MainHeroHolderService>(),
                c.Resolve<GameplayPopupService>(),
                c.Resolve<ICoroutinesPerformer>(),
                c.Resolve<IPauseService>());
        }

        private static TimeScalePauseService CreateTimeScalePauseService(DIContainer c)
            => new TimeScalePauseService();

        private static LootFactory CreateLootFactory(DIContainer c) => new LootFactory(c);

        private static DropLootService CreateDropLootService(DIContainer c)
        {
            return new DropLootService(
                c.Resolve<ConfigsProviderService>().GetConfig<LootListConfig>(),
                c.Resolve<LootFactory>());
        }

        private static LootPullingService CreateLootPullingService(DIContainer c)
            => new LootPullingService(c.Resolve<EntitiesLifeContext>());
    }
}
