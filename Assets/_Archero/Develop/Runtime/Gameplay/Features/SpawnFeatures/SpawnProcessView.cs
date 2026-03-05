using System;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.SpawnFeatures
{
    public class SpawnProcessView : EntityView
    {
        private readonly int _spawningProcessKey = Animator.StringToHash("InSpawnProcess");

        [SerializeField] private Animator _animator;
        [SerializeField] private ParticleSystem _spawnEffectPrefab;

        private ReactiveVariable<bool> _inSpawnProcess;
        private Transform _entityTransform;

        private IDisposable _inSpawnProcessChangedDisposable;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityStartedWork(Entity entity)
        {
            _inSpawnProcess = entity.InSpawnProcess;
            _entityTransform = entity.Transform;

            _inSpawnProcessChangedDisposable = _inSpawnProcess.Subscribe(OnSpawnProcessChanged);
            UpdateSpawnProcessKey(_inSpawnProcess.Value);
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);
            _inSpawnProcessChangedDisposable?.Dispose();
        }

        private void OnSpawnProcessChanged(bool oldValue, bool newValue) => UpdateSpawnProcessKey(newValue);

        private void UpdateSpawnProcessKey(bool value)
        {
            _animator.SetBool(_spawningProcessKey, value);

            if (value)
                Instantiate(_spawnEffectPrefab, _entityTransform.position, _spawnEffectPrefab.transform.rotation, null);
        }
    }
}
