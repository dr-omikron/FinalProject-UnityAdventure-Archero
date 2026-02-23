using System;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Infrastructure.DI;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.Gameplay.Features.MainHero
{
    public class MainHeroHolderService : IInitializable, IDisposable
    {
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly ReactiveEvent<Entity> _heroRegister =  new ReactiveEvent<Entity>();
        private Entity _mainHero;

        public MainHeroHolderService(EntitiesLifeContext entitiesLifeContext)
        {
            _entitiesLifeContext = entitiesLifeContext;
        }

        public IReadOnlyEvent<Entity> HeroRegister => _heroRegister;
        public Entity MainHero => _mainHero;

        public void Initialize()
        {
            _entitiesLifeContext.Added += OnEntityAdded;
        }

        private void OnEntityAdded(Entity entity)
        {
            if (entity.HasComponent<IsMainHero>())
            {
                _entitiesLifeContext.Added -= OnEntityAdded;
                _mainHero = entity;
                _heroRegister?.Invoke(_mainHero);
            }
        }

        public void Dispose()
        {
            _entitiesLifeContext.Added -= OnEntityAdded;
        }
    }
}
