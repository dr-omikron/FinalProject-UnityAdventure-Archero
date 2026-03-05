using System;
using System.Collections.Generic;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.Features.MainHero;
using _Archero.Develop.Runtime.UI.CommonViews;
using _Archero.Develop.Runtime.UI.Core;
using UnityEngine;

namespace _Archero.Develop.Runtime.UI.Gameplay.HealthDisplay
{
    public class EntityHealthDisplayPresenter : IPresenter
    {
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly EntitiesHealthDisplay _view;
        private readonly GameplayPresentersFactory _gameplayPresentersFactory;
        private readonly ViewsFactory _viewsFactory;

        private readonly Dictionary<Entity, EntitiesHealthBarInfo> _entitiesToHealthBarInfo = new Dictionary<Entity, EntitiesHealthBarInfo>();

        public EntityHealthDisplayPresenter(
            EntitiesLifeContext entitiesLifeContext, 
            EntitiesHealthDisplay view, 
            GameplayPresentersFactory gameplayPresentersFactory, 
            ViewsFactory viewsFactory)
        {
            _entitiesLifeContext = entitiesLifeContext;
            _view = view;
            _gameplayPresentersFactory = gameplayPresentersFactory;
            _viewsFactory = viewsFactory;
        }

        public void Initialize()
        {
            _entitiesLifeContext.Added += OnEntityAdded;
            _entitiesLifeContext.Released += OnEntityReleased;

            foreach (Entity entity in _entitiesLifeContext.Entities)
                OnEntityAdded(entity);
        }

        private void OnEntityAdded(Entity entity)
        {
            if (entity.TryGetHealthBarPoint(out Transform healthBarPoint))
            {
                BarWithText healthBarView = null;

                if (entity.HasComponent<IsMainHero>())
                    healthBarView = _viewsFactory.Create<BarWithText>(ViewIDs.HeroHealthBar);
                else
                    healthBarView = _viewsFactory.Create<BarWithText>(ViewIDs.SimpleHealthBar);

                _view.Add(healthBarView);

                EntityHealthPresenter entityHealthPresenter =
                    _gameplayPresentersFactory.CreateEntityHealthPresenter(entity, healthBarView);

                entityHealthPresenter.Initialize();

                IDisposable removeReason = entity.IsDead.Subscribe((oldValue, isDead) =>
                {
                    if (isDead)
                        RemoveHealthBarFor(entity);
                });

                _entitiesToHealthBarInfo.Add(entity, new EntitiesHealthBarInfo(healthBarPoint, removeReason, entityHealthPresenter));
            }
        }

        public void LateUpdate()
        {
            foreach (KeyValuePair<Entity, EntitiesHealthBarInfo> info in _entitiesToHealthBarInfo)
                _view.UpdatePositionFor(info.Value.HealthPresenter.Bar, info.Value.HealthBarPoint.position);
        }

        private void RemoveHealthBarFor(Entity entity)
        {
            EntitiesHealthBarInfo healthBarInfo = _entitiesToHealthBarInfo[entity];
            DisposeFor(healthBarInfo);
            _entitiesToHealthBarInfo.Remove(entity);
        }

        private void DisposeFor(EntitiesHealthBarInfo info)
        {
            info.RemoveReason.Dispose();

            _view.Remove(info.HealthPresenter.Bar);
            _viewsFactory.Release(info.HealthPresenter.Bar);

            info.HealthPresenter.Dispose();
        }

        private void OnEntityReleased(Entity entity)
        {
            if (_entitiesToHealthBarInfo.ContainsKey(entity))
                RemoveHealthBarFor(entity);
        }

        public void Dispose()
        {
            _entitiesLifeContext.Added -= OnEntityAdded;
            _entitiesLifeContext.Released -= OnEntityReleased;

            foreach (EntitiesHealthBarInfo info in _entitiesToHealthBarInfo.Values)
                DisposeFor(info);

            _entitiesToHealthBarInfo.Clear();
        }

        private class EntitiesHealthBarInfo
        {
            public EntitiesHealthBarInfo(
                Transform healthBarPoint, 
                IDisposable removeReason, 
                EntityHealthPresenter healthPresenter)
            {
                HealthBarPoint = healthBarPoint;
                RemoveReason = removeReason;
                HealthPresenter = healthPresenter;
            }

            public Transform HealthBarPoint { get; }
            public IDisposable RemoveReason { get; }
            public EntityHealthPresenter HealthPresenter { get; }
        }
    }
}
