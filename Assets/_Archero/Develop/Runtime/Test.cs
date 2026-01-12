using System.Collections;
using _Archero.Develop.Runtime.Infrastructure.DI;
using _Archero.Develop.Runtime.Utilities.AssetsManagement;
using _Archero.Develop.Runtime.Utilities.ConfigsManagement;
using _Archero.Develop.Runtime.Utilities.CoroutinesManagement;
using UnityEngine;

namespace _Archero.Develop.Runtime
{
    public class Test : MonoBehaviour
    {
        private DIContainer _container;

        private void Awake()
        {
            _container = new DIContainer();
            _container.RegisterAsSingle<ICoroutinesPerformer>(CreateCoroutinesPerformer);
            _container.RegisterAsSingle(CreateConfigsProviderService);
            _container.RegisterAsSingle(CreateResourcesAssetsLoader);

            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            coroutinesPerformer.StartPerform(LoadConfigs());
        }

        private CoroutinesPerformer CreateCoroutinesPerformer(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            CoroutinesPerformer coroutinesPerformerPrefab = 
                resourcesAssetsLoader.Load<CoroutinesPerformer>("Utilities/CoroutinesPerformer");

            return Instantiate(coroutinesPerformerPrefab);
        }

        private ConfigsProviderService CreateConfigsProviderService(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();
            ResourcesConfigsLoader resourcesConfigsLoader = new ResourcesConfigsLoader(resourcesAssetsLoader);
            return new ConfigsProviderService(resourcesConfigsLoader);
        }

        private ResourcesAssetsLoader CreateResourcesAssetsLoader(DIContainer c) => new ResourcesAssetsLoader();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ConfigsProviderService configsProviderService = _container.Resolve<ConfigsProviderService>();
                TestConfig config = configsProviderService.GetConfig<TestConfig>();
                Debug.Log("TestConfig: " + config.Damage);
            }
        }

        IEnumerator LoadConfigs()
        {
            Debug.Log("StartLoadConfigs");
            ConfigsProviderService configsProviderService = _container.Resolve<ConfigsProviderService>();
            yield return configsProviderService.LoadAsync();
            Debug.Log("EndLoadConfigs");
        }
    }
}
