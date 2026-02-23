using _Archero.Develop.Runtime.Gameplay.Features.InputFeatures;
using _Archero.Develop.Runtime.Gameplay.Infrastructure;
using _Archero.Develop.Runtime.Meta.Features.LevelsProgression;
using _Archero.Develop.Runtime.Utilities.CoroutinesManagement;
using _Archero.Develop.Runtime.Utilities.DataManagement.DataProviders;
using _Archero.Develop.Runtime.Utilities.SceneManagement;
using _Archero.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.States
{
    public class WinState : EndGameState, IUpdatableState
    {
        private readonly LevelsProgressionService _levelsProgressionService;
        private readonly GameplayInputArgs _gameplayInputArgs;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        public WinState(
            IInputService inputService, 
            LevelsProgressionService levelsProgressionService, 
            GameplayInputArgs gameplayInputArgs, 
            PlayerDataProvider playerDataProvider, 
            SceneSwitcherService sceneSwitcherService, 
            ICoroutinesPerformer coroutinesPerformer) : base(inputService)
        {
            _levelsProgressionService = levelsProgressionService;
            _gameplayInputArgs = gameplayInputArgs;
            _playerDataProvider = playerDataProvider;
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("WIN!");
            _levelsProgressionService.AddLevelToCompleted(_gameplayInputArgs.LevelNumber);
            _coroutinesPerformer.StartPerform(_playerDataProvider.SaveAsync());
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
            }
        }
    }
}
