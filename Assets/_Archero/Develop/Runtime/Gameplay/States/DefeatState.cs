using _Archero.Develop.Runtime.Gameplay.Features.InputFeatures;
using _Archero.Develop.Runtime.UI.Gameplay;
using _Archero.Develop.Runtime.Utilities.StateMachineCore;

namespace _Archero.Develop.Runtime.Gameplay.States
{
    public class DefeatState : EndGameState, IUpdatableState
    {
        private readonly GameplayPopupService _gameplayPopupService;

        public DefeatState(
            IInputService inputService, 
            GameplayPopupService gameplayPopupService) : base(inputService)
        {
            _gameplayPopupService = gameplayPopupService;
        }

        public override void Enter()
        {
            base.Enter();
            _gameplayPopupService.OpenDefeatPopup();
        }

        public void Update(float deltaTime) { }
    }
}
