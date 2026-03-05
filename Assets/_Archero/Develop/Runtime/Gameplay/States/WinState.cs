using _Archero.Develop.Runtime.Gameplay.Features.InputFeatures;
using _Archero.Develop.Runtime.Gameplay.Infrastructure;
using _Archero.Develop.Runtime.Meta.Features.LevelsProgression;
using _Archero.Develop.Runtime.UI.Gameplay;
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
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly GameplayPopupService _gameplayPopupService;

        public WinState(
            IInputService inputService, 
            LevelsProgressionService levelsProgressionService, 
            GameplayInputArgs gameplayInputArgs, 
            PlayerDataProvider playerDataProvider, 
            ICoroutinesPerformer coroutinesPerformer, 
            GameplayPopupService gameplayPopupService) : base(inputService)
        {
            _levelsProgressionService = levelsProgressionService;
            _gameplayInputArgs = gameplayInputArgs;
            _playerDataProvider = playerDataProvider;
            _coroutinesPerformer = coroutinesPerformer;
            _gameplayPopupService = gameplayPopupService;
        }

        public override void Enter()
        {
            base.Enter();

            _levelsProgressionService.AddLevelToCompleted(_gameplayInputArgs.LevelNumber);
            _coroutinesPerformer.StartPerform(_playerDataProvider.SaveAsync());
            _gameplayPopupService.OpenWinPopup();
        }

        public void Update(float deltaTime) { }
    }
}
