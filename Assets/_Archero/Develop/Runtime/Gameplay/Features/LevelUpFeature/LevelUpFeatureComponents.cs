using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.Gameplay.Features.LevelUpFeature
{
    public class Experience : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class Level : IEntityComponent
    {
        public ReactiveVariable<int> Value;
    }
}
