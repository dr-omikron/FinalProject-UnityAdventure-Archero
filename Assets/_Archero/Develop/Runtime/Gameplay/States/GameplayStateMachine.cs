using System;
using System.Collections.Generic;
using _Archero.Develop.Runtime.Utilities.StateMachineCore;

namespace _Archero.Develop.Runtime.Gameplay.States
{
    public class GameplayStateMachine : StateMachine<IUpdatableState>
    {
        public GameplayStateMachine(List<IDisposable> disposables) : base(disposables)
        {
        }

        public GameplayStateMachine() : base(new List<IDisposable>())
        {
        }

        protected override void UpdateLogic(float deltaTime)
        {
            base.UpdateLogic(deltaTime);
            CurrentState.Update(deltaTime);
        }
    }
}
