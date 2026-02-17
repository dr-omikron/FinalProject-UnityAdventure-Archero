using _Archero.Develop.Runtime.Utilities.StateMachineCore;

namespace _Archero.Develop.Runtime.Gameplay.Features.AI.States
{
    public class EmptyState : State, IUpdatableState
    {
        public void Update(float deltaTime) { }
    }
}
