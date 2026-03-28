using System.Collections.Generic;

namespace _Archero.Develop.Runtime.Gameplay.Features.StatsFeature
{
    public interface IStatsEffect
    {
        void ApplyTo(Dictionary<StatTypes, float> stats);
    }
}
