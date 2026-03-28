using _Archero.Develop.Runtime.Gameplay.Features.InputFeatures;
using _Archero.Develop.Runtime.Gameplay.Features.PauseFeature;
using _Archero.Develop.Runtime.Utilities.StateMachineCore;

namespace _Archero.Develop.Runtime.Gameplay.States
{
    public abstract class EndGameState : State
    {
        private readonly IInputService _inputService;
        private readonly IPauseService _pauseService;

        protected EndGameState(IInputService inputService, IPauseService pauseService)
        {
            _inputService = inputService;
            _pauseService = pauseService;
        }

        public override void Enter()
        {
            base.Enter();

            _inputService.IsEnabled = false;
            _pauseService.Pause();
        }

        public override void Exit()
        {
            base.Exit();

            _inputService.IsEnabled = true;
            _pauseService.Unpause();
        }
    }
}
