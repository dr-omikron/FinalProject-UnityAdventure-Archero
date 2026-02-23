using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.Gameplay.Features.MainHero
{
    public class IsMainHero : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
}
