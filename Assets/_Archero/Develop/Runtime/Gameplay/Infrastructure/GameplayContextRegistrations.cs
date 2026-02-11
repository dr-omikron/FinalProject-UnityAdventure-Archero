using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Archero.Develop.Runtime.Infrastructure.DI;
using _Archero.Develop.Runtime.Utilities.AssetsManagement;

namespace _Archero.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            container.RegisterAsSingle(CreateEntitiesFactory);
            container.RegisterAsSingle(CreateEntitiesLifeContext);
            container.RegisterAsSingle(CreateMonoEntityFactory).NonLazy();
            container.RegisterAsSingle(CreateColliderRegistryService);
        }

        private static EntitiesLifeContext CreateEntitiesLifeContext(DIContainer c)
            => new EntitiesLifeContext();

        private static EntitiesFactory CreateEntitiesFactory(DIContainer c)
            => new EntitiesFactory(c);

        private static MonoEntityFactory CreateMonoEntityFactory(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();
            EntitiesLifeContext entitiesLifeContext = c.Resolve<EntitiesLifeContext>();
            ColliderRegistryService colliderRegistryService = c.Resolve<ColliderRegistryService>();

            return new MonoEntityFactory(resourcesAssetsLoader, entitiesLifeContext, colliderRegistryService);
        }
        
        private static ColliderRegistryService CreateColliderRegistryService(DIContainer c)
            => new ColliderRegistryService();
    }
}
