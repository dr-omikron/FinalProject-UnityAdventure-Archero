using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.Gameplay.Features.LifeCycle
{
    public class CurrentHealth : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class MaxHealth : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class IsDead : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }

    public class DeathProcessInitialTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class DeathProcessCurrentTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class InDeathProcess : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
}
