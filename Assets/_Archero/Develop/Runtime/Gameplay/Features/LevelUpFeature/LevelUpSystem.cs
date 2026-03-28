using System;
using _Archero.Develop.Runtime.Configs.Gameplay;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.Gameplay.Features.LevelUpFeature
{
    public class LevelUpSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveVariable<int> _level;
        private ReactiveVariable<float> _experience;
        private readonly ExperienceForUpgradeLevelConfig _config;

        private IDisposable _experienceChangedDisposable;

        public LevelUpSystem(ExperienceForUpgradeLevelConfig config)
        {
            _config = config;
        }
        
        public float CurrentLimitForExp => _config.GetExperienceFor(_level.Value);

        public void OnInit(Entity entity)
        {
            _experience = entity.Experience;
            _level = entity.Level;

            _experienceChangedDisposable = _experience.Subscribe(OnExperienceChanged);
        }

        private void OnExperienceChanged(float arg1, float newExp)
        {
            while (newExp >= CurrentLimitForExp && _level.Value < _config.MaxLevel)
            {
                newExp -= CurrentLimitForExp;
                _level.Value++;
            }

            _experience.Value = newExp;
        }

        public void OnDispose() => _experienceChangedDisposable.Dispose();
    }
}
