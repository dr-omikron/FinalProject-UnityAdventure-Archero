using System.Collections.Generic;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.Gameplay.Features.StatsFeature
{
    public class AttackPerSecondStatSynchronizerSystem : IInitializableSystem, IUpdateableSystem
    {
        private ReactiveVariable<float> _attackPerSecond;
        private Dictionary<StatTypes, float> _modifiedStats;

        public void OnInit(Entity entity)
        {
            _attackPerSecond = entity.AttackPerSeconds;
            _modifiedStats = entity.ModifiedStats;
        }

        public void OnUpdate(float deltaTime)
        {
            float tempValue = _modifiedStats[StatTypes.AttackPerSecond];

            if(tempValue < 0)
                tempValue = 0;

            _attackPerSecond.Value = tempValue;
        }
    }
}
