using System.Collections.Generic;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;

namespace _Archero.Develop.Runtime.Gameplay.Features.AI.States
{
    public interface ITargetSelector
    {
        Entity SelectTargetFrom(IEnumerable<Entity> entity);
    }
}
