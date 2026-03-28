using System.Collections.Generic;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.Gameplay.Features.StatsFeature
{
    public class MoveSpeedStatSynchronizerSystem : IInitializableSystem, IUpdateableSystem
    {
        private ReactiveVariable<float> _speed;
        private Dictionary<StatTypes, float> _modifiedStats;

        public void OnInit(Entity entity)
        {
            _speed = entity.MoveSpeed;
            _modifiedStats = entity.ModifiedStats;
        }

        public void OnUpdate(float deltaTime)
        {
            float tempValue = _modifiedStats[StatTypes.MoveSpeed];

            if (tempValue < 0)
                tempValue = 0;

            _speed.Value = tempValue;
        }
    }
}
