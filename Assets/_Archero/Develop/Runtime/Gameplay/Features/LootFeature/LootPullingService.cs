using System;
using System.Collections.Generic;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Infrastructure.DI;
using _Archero.Develop.Runtime.Utilities.Reactive;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Archero.Develop.Runtime.Gameplay.Features.LootFeature
{
    public class LootPullingService : IInitializable, IDisposable
    {
        private readonly ReactiveVariable<bool> _allCollected = new ReactiveVariable<bool>();
        private readonly List<Entity> _loot = new List<Entity>();
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private bool _isActivated;

        public LootPullingService(EntitiesLifeContext entitiesLifeContext)
        {
            _entitiesLifeContext = entitiesLifeContext;
        }

        public IReadOnlyVariable<bool> AllCollected => _allCollected;

        public void Initialize()
        {
            _entitiesLifeContext.Added += OnEntityAdded;
            _entitiesLifeContext.Released += OnEntityReleased;
        }

        public void Dispose()
        {
            _entitiesLifeContext.Added -= OnEntityAdded;
            _entitiesLifeContext.Released -= OnEntityReleased;
        }

        public void PullTo(Entity entity)
        {
            if (_isActivated)
                throw new InvalidOperationException();

            _isActivated = true;

            if (_loot.Count == 0)
            {
                _allCollected.Value = true;
                return;
            }

            foreach (Entity loot in _loot)
            {
                loot.CurrentTarget.Value = entity;
                loot.IsPullingProcess.Value = true;
            }
        }

        public void Reset()
        {
            _isActivated = false;
            _allCollected.Value = false;
        }

        private void OnEntityAdded(Entity entity)
        {
            if(entity.HasComponent<IsPullable>() == false)
                return;

            _loot.Add(entity);

            Transform lootTransform = entity.Transform;

            Vector2 randomOffset = Random.insideUnitCircle;
            Vector3 offset = new Vector3(randomOffset.x, 0, randomOffset.y);
            Vector3 endJumpPosition = lootTransform.position + offset;

            lootTransform
                .DOJump(endJumpPosition, 2, 1, 0.7f)
                .SetEase(Ease.OutBounce)
                .OnComplete(() => entity.InSpawnProcess.Value = false)
                .Play();
        }

        private void OnEntityReleased(Entity entity)
        {
            bool lootRemoved = _loot.Remove(entity);

            if (lootRemoved && _loot.Count == 0)
                _allCollected.Value = true;
        }

    }
}
