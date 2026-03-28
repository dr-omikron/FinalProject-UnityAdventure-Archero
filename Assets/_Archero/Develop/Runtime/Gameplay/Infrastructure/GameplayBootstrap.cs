using System;
using System.Collections;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.Features.AI;
using _Archero.Develop.Runtime.Gameplay.Features.MainHero;
using _Archero.Develop.Runtime.Gameplay.States;
using _Archero.Develop.Runtime.Infrastructure;
using _Archero.Develop.Runtime.Infrastructure.DI;
using _Archero.Develop.Runtime.UI.Gameplay;
using _Archero.Develop.Runtime.Utilities.CoroutinesManagement;
using _Archero.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;
        private EntitiesLifeContext _entitiesLifeContext;
        private AIBrainContext _aiBrainContext;
        private GameplayStatesContext _gameplayStatesContext;
        private GameplayScreenPresenter _gameplayScreenPresenter;

        private MainHeroHolderService _mainHeroHolderService;

        public override void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");

            _inputArgs = gameplayInputArgs;

            GameplayContextRegistrations.Process(_container, gameplayInputArgs);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Loaded level number: " + _inputArgs.LevelNumber);
            Debug.Log("Gameplay Scene Initialized");

            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _aiBrainContext = _container.Resolve<AIBrainContext>();
            _gameplayStatesContext = _container.Resolve<GameplayStatesContext>();
            _gameplayScreenPresenter = _container.Resolve<GameplayScreenPresenter>();
            _container.Resolve<MainHeroFactory>().Create(Vector3.zero);

            _mainHeroHolderService = _container.Resolve<MainHeroHolderService>();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Gameplay Scene Started");
            _gameplayStatesContext.Run();
        }

        private void Update()
        {
            _aiBrainContext?.Update(Time.deltaTime);
            _entitiesLifeContext?.Update(Time.deltaTime);
            _gameplayStatesContext?.Update(Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
                ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
                coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
            }

            if (Input.GetKeyDown(KeyCode.Space))
                _mainHeroHolderService.MainHero.Experience.Value += 1000;
        }

        private void LateUpdate()
        {
            _gameplayScreenPresenter?.LateUpdate();
        }
    }
}
