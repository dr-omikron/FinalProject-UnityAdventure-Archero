using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Archero.Develop.Runtime.Configs.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Abilities/AdditionalDirectionsShotAbilityConfig", fileName = "AdditionalDirectionsShotAbilityConfig")]
    public class AdditionalDirectionsShotAbilityConfig : AbilityConfig
    {
        [SerializeField] private List<Config> _additionalArrowsByLevel;
        public List<DirectionShotConfig> GetBy(int level) => _additionalArrowsByLevel[level - 1].DirectionShotConfigs;
        public override int MaxLevel => _additionalArrowsByLevel.Count;

        [Serializable]
        private class Config
        {
            [field: SerializeField] public List<DirectionShotConfig> DirectionShotConfigs { get; private set; }
        }
    }

    [Serializable]
    public class DirectionShotConfig
    {
        [field: SerializeField] public int Angle { get; private set; }
        [field: SerializeField] public int NumberOfProjectile { get; private set; }
    }
}
