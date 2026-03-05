using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.Gameplay.Features.SpawnFeatures
{
    public class SpawnInitialTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class SpawnCurrentTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class InSpawnProcess : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
}
