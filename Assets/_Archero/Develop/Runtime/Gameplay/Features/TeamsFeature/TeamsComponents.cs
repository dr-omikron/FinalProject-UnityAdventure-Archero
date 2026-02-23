using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.Gameplay.Features.TeamsFeature
{
    public class Team : IEntityComponent
    {
        public ReactiveVariable<Teams> Value;
    }
}
