using System.Collections.Generic;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Systems;

namespace _Archero.Develop.Runtime.Gameplay.Features.StatsFeature
{
    public class StatEffectsApplierSystem : IInitializableSystem, IDisposableSystem
    {
        private StatsEffectList _statsEffects;
        private Dictionary<StatTypes, float> _baseStats;
        private Dictionary<StatTypes, float> _modifiedStats;

        public void OnInit(Entity entity)
        {
            _statsEffects = entity.StatsEffects;
            _baseStats = entity.BaseStats;
            _modifiedStats = entity.ModifiedStats;

            _statsEffects.Added += OnStatEffectAdded;
            _statsEffects.Removed += OnStatEffectRemoved;

            RecalculateStats();
        }

        public void OnDispose()
        {
            _statsEffects.Added -= OnStatEffectAdded;
            _statsEffects.Removed -= OnStatEffectRemoved;
        }

        private void OnStatEffectRemoved(IStatsEffect statEffect) => RecalculateStats();
        private void OnStatEffectAdded(IStatsEffect statEffect) => RecalculateStats();

        private void RecalculateStats()
        {
            foreach (StatTypes stat in _baseStats.Keys)
                _modifiedStats[stat] = _baseStats[stat];

            foreach (IStatsEffect statsEffect in _statsEffects.Elements)
                statsEffect.ApplyTo(_modifiedStats);
        }

    }
}
