using _Archero.Develop.Runtime.Gameplay.Features.InputFeatures;
using _Archero.Develop.Runtime.Utilities.StateMachineCore;

namespace _Archero.Develop.Runtime.Gameplay.States
{
    public abstract class EndGameState : State
    {
        private readonly IInputService _inputService;

        protected EndGameState(IInputService inputService)
        {
            _inputService = inputService;
        }

        public override void Enter()
        {
            base.Enter();

            _inputService.IsEnabled = false;
        }

        public override void Exit()
        {
            base.Exit();

            _inputService.IsEnabled = true;
        }
    }
}
