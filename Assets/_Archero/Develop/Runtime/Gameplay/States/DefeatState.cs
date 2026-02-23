using _Archero.Develop.Runtime.Gameplay.Features.InputFeatures;
using _Archero.Develop.Runtime.Utilities.CoroutinesManagement;
using _Archero.Develop.Runtime.Utilities.SceneManagement;
using _Archero.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.States
{
    public class DefeatState : EndGameState, IUpdatableState
    {
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        public DefeatState(
            IInputService inputService, 
            SceneSwitcherService sceneSwitcherService, 
            ICoroutinesPerformer coroutinesPerformer) : base(inputService)
        {
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("DEFEAT!");
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
