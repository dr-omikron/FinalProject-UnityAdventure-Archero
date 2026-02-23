using System;
using _Archero.Develop.Runtime.Configs.Gameplay.Stages;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.Features.Enemies;
using _Archero.Develop.Runtime.Infrastructure.DI;

namespace _Archero.Develop.Runtime.Gameplay.Features.StagesFeature
{
    public class StagesFactory
    {
        private readonly DIContainer _container;

        public StagesFactory(DIContainer container)
        {
            _container = container;
        }

        public IStage Create(StageConfig stageConfig)
        {
            switch (stageConfig)
            {
                case ClearAllEnemiesStageConfig clearAllEnemiesStageConfig:
                    return new ClearAllEnemiesStage(
                        clearAllEnemiesStageConfig,
                        _container.Resolve<EnemiesFactory>(),
                        _container.Resolve<EntitiesLifeContext>());

                default:
                    throw new ArgumentException($"Not supported stage type {stageConfig.GetType()}");
            }
        }
    }
}
