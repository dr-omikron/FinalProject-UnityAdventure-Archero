using System;
using _Archero.Develop.Runtime.Configs.Gameplay.Levels;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.Gameplay.Features.StagesFeature
{
    public class StageProviderService : IDisposable
    {
        private readonly ReactiveVariable<int> _currentStageNumber =  new ReactiveVariable<int>();
        private readonly ReactiveVariable<StageResult> _currentStageResult =  new ReactiveVariable<StageResult>();

        private readonly LevelConfig _levelConfig;
        private readonly StagesFactory _stagesFactory;

        private IStage _currentStage;
        
        private IDisposable _stageEndedDisposable;

        public StageProviderService(LevelConfig levelConfig, StagesFactory stagesFactory)
        {
            _levelConfig = levelConfig;
            _stagesFactory = stagesFactory;
        }

        public IReadOnlyVariable<int> CurrentStageNumber => _currentStageNumber;
        public IReadOnlyVariable<StageResult> CurrentStageResult => _currentStageResult;
        public int StagesCount => _levelConfig.StageConfigs.Count;

        public bool HasNextStage => _currentStageNumber.Value < StagesCount;

        public void SwitchToNext()
        {
            if(HasNextStage == false)
                throw new InvalidOperationException();
            
            if(_currentStage != null)
                CleanupCurrent();

            _currentStageNumber.Value++;
            _currentStageResult.Value = StageResult.Uncompleted;
            _currentStage = _stagesFactory.Create(_levelConfig.StageConfigs[_currentStageNumber.Value - 1]);
        }

        public void StartCurrent()
        {
            _stageEndedDisposable = _currentStage.Completed.Subscribe(OnStageCompleted);
            _currentStage.Start();
        }

        private void OnStageCompleted()
        {
            _currentStageResult.Value = StageResult.Completed;
        }

        public void UpdateCurrent(float deltaTime) => _currentStage.Update(deltaTime);

        public void CleanupCurrent() => _currentStage.Cleanup();

        public void Dispose()
        {
            _currentStage?.Dispose();
            _stageEndedDisposable?.Dispose();
        }
    }
}
