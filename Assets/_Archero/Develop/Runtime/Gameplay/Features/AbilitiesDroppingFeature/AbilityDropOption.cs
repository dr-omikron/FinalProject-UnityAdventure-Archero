using _Archero.Develop.Runtime.Configs.Abilities;

namespace _Archero.Develop.Runtime.Gameplay.Features.AbilitiesDroppingFeature
{
    public class AbilityDropOption
    {
        public AbilityDropOption(AbilityConfig config, int level)
        {
            Config = config;
            Level = level;
        }

        public AbilityConfig Config { get; }
        public int Level { get; }
    }
}
