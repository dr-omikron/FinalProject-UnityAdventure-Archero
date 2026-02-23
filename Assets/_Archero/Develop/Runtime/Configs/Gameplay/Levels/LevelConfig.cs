using System.Collections.Generic;
using _Archero.Develop.Runtime.Configs.Gameplay.Stages;
using UnityEngine;

namespace _Archero.Develop.Runtime.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Levels/NewLevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private List<StageConfig> _stageConfigs;

        public IReadOnlyList<StageConfig> StageConfigs => _stageConfigs;
    }
}
