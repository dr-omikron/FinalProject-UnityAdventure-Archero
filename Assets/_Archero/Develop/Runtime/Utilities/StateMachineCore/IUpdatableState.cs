namespace _Archero.Develop.Runtime.Utilities.StateMachineCore
{
    public interface IUpdatableState : IState
    {
        void Update(float deltaTime);
    }
}
