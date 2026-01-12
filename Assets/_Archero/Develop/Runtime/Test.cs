using System.Collections;
using _Archero.Develop.Runtime.Utilities.AssetsManagement;
using _Archero.Develop.Runtime.Utilities.ConfigsManagement;
using _Archero.Develop.Runtime.Utilities.CoroutinesManagement;
using UnityEngine;

namespace _Archero.Develop.Runtime
{
    public class Test : MonoBehaviour
    {
        private ResourcesAssetsLoader _resourcesAssetsLoader;
        private ICoroutinesPerformer _coroutinesPerformer;
        private ConfigsProviderService _configsProviderService;

        private void Awake()
        {
            _resourcesAssetsLoader = new ResourcesAssetsLoader();
            _coroutinesPerformer = CreateCoroutinesPerformer();
            _configsProviderService = CreateConfigsProviderService();

            _coroutinesPerformer.StartPerform(LoadConfigs());
        }

        private ConfigsProviderService CreateConfigsProviderService()
        {
            ResourcesConfigsLoader resourcesConfigsLoader = new ResourcesConfigsLoader(_resourcesAssetsLoader);
            return new ConfigsProviderService(resourcesConfigsLoader);
        }

        private CoroutinesPerformer CreateCoroutinesPerformer()
        {
            CoroutinesPerformer coroutinesPerformerPrefab = 
                _resourcesAssetsLoader.Load<CoroutinesPerformer>("Utilities/CoroutinesPerformer");

            return Instantiate(coroutinesPerformerPrefab);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TestConfig config = _configsProviderService.GetConfig<TestConfig>();
                Debug.Log("TestConfig: " + config.Damage);
            }
        }

        IEnumerator LoadConfigs()
        {
            Debug.Log("StartLoadConfigs");
            yield return _configsProviderService.LoadAsync();
            Debug.Log("EndLoadConfigs");
        }
    }
}
