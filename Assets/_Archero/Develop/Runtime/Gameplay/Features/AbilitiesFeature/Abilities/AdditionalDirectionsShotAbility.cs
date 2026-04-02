using System;
using System.Collections.Generic;
using _Archero.Develop.Runtime.Configs.Abilities;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.Features.Attack.Shoot;

namespace _Archero.Develop.Runtime.Gameplay.Features.AbilitiesFeature.Abilities
{
    public class AdditionalDirectionsShotAbility : Ability, IDisposable
    {
        private readonly AdditionalDirectionsShotAbilityConfig _config;
        private readonly Entity _entity;

        private IDisposable _currentLevelChangedDisposable;

        public AdditionalDirectionsShotAbility(
            AdditionalDirectionsShotAbilityConfig config,
            Entity entity,
            int currentLevel) : base(config.ID, currentLevel, config.MaxLevel)
        {
            _config = config;
            _entity = entity;
        }

        public override void Activate()
        {
            for (int i = 0; i < CurrentLevel.Value; i++)
                AddShotDirectionBy(i + 1);

            _currentLevelChangedDisposable = CurrentLevel.Subscribe(OnCurrentLevelChanged);
        }

        private void OnCurrentLevelChanged(int prevLevel, int newLevel)
        {
            for (int i = prevLevel; i < newLevel; i++)
                AddShotDirectionBy(i + 1);
        }

        private void AddShotDirectionBy(int level)
        {
            List<DirectionShotConfig> directionShotConfigs = _config.GetBy(level);
            InstantShootingDirectionArgs shootingDirectionArgs = _entity.InstantShootingDirection;

            foreach (DirectionShotConfig directionShotConfig in directionShotConfigs)
                shootingDirectionArgs.Add(new InstantShotDirectionArgs(directionShotConfig.Angle, directionShotConfig.NumberOfProjectile));
        }

        public void Dispose()
        {
            _currentLevelChangedDisposable.Dispose();
        }
    }
}
