using System.Collections.Generic;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;

namespace _Archero.Develop.Runtime.Gameplay.Features.StatsFeature
{
    public class BaseStats : IEntityComponent
    {
        public Dictionary<StatTypes, float> Value;
    }

    public class ModifiedStats : IEntityComponent
    {
        public Dictionary<StatTypes, float> Value;
    }

    public class StatsEffects : IEntityComponent
    {
        public StatsEffectList Value;
    }
}
