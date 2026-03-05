using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.Gameplay.Features.SpawnFeatures
{
    public class SpawnProcessTimerSystem : IInitializableSystem, IUpdateableSystem
    {
        private ReactiveVariable<float> _initialTime;
        private ReactiveVariable<float> _currentTime;
        private ReactiveVariable<bool> _inSpawnProcess;

        public void OnInit(Entity entity)
        {
            _initialTime = entity.SpawnInitialTime;
            _currentTime = entity.SpawnCurrentTime;
            _inSpawnProcess = entity.InSpawnProcess;

            _currentTime.Value = 0;
            _inSpawnProcess.Value = true;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inSpawnProcess.Value == false)
                return;

            _currentTime.Value += deltaTime;

            if (_currentTime.Value >= _initialTime.Value)
                _inSpawnProcess.Value = false;
        }
    }
}
