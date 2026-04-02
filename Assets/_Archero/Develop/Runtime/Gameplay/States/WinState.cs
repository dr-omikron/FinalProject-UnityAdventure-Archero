using _Archero.Develop.Runtime.Gameplay.Features.InputFeatures;
using _Archero.Develop.Runtime.Gameplay.Features.MainHero;
using _Archero.Develop.Runtime.Gameplay.Features.PauseFeature;
using _Archero.Develop.Runtime.Gameplay.Infrastructure;
using _Archero.Develop.Runtime.Meta.Features.LevelsProgression;
using _Archero.Develop.Runtime.Meta.Features.Wallet;
using _Archero.Develop.Runtime.UI.Gameplay;
using _Archero.Develop.Runtime.Utilities.CoroutinesManagement;
using _Archero.Develop.Runtime.Utilities.DataManagement.DataProviders;
using _Archero.Develop.Runtime.Utilities.StateMachineCore;

namespace _Archero.Develop.Runtime.Gameplay.States
{
    public class WinState : EndGameState, IUpdatableState
    {
        private readonly LevelsProgressionService _levelsProgressionService;
        private readonly GameplayInputArgs _gameplayInputArgs;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly GameplayPopupService _gameplayPopupService;
        
        private readonly WalletService _walletService;
        private readonly MainHeroHolderService _mainHeroHolderService;

        public WinState(
            IInputService inputService, 
            IPauseService pauseService,
            LevelsProgressionService levelsProgressionService, 
            GameplayInputArgs gameplayInputArgs, 
            PlayerDataProvider playerDataProvider, 
            ICoroutinesPerformer coroutinesPerformer, 
            GameplayPopupService gameplayPopupService, 
            WalletService walletService, 
            MainHeroHolderService mainHeroHolderService) : base(inputService, pauseService)
        {
            _levelsProgressionService = levelsProgressionService;
            _gameplayInputArgs = gameplayInputArgs;
            _playerDataProvider = playerDataProvider;
            _coroutinesPerformer = coroutinesPerformer;
            _gameplayPopupService = gameplayPopupService;
            _walletService = walletService;
            _mainHeroHolderService = mainHeroHolderService;
        }

        public override void Enter()
        {
            base.Enter();

            _walletService.Add(CurrencyType.Gold, _mainHeroHolderService.MainHero.Coins.Value);
            _levelsProgressionService.AddLevelToCompleted(_gameplayInputArgs.LevelNumber);
            _coroutinesPerformer.StartPerform(_playerDataProvider.SaveAsync());
            _gameplayPopupService.OpenWinPopup();
        }

        public void Update(float deltaTime) { }
    }
}
