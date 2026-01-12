using System.Collections;
using _Archero.Develop.Runtime.Utilities.AssetsManagement;
using _Archero.Develop.Runtime.Utilities.CoroutinesManagement;
using UnityEngine;

namespace _Archero.Develop.Runtime
{
    public class Test : MonoBehaviour
    {
        private ResourcesAssetsLoader _resourcesAssetsLoader;
        private ICoroutinesPerformer _coroutinesPerformer;

        private void Awake()
        {
            _resourcesAssetsLoader = new ResourcesAssetsLoader();
            _coroutinesPerformer = CreateCoroutinesPerformer();
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
                _coroutinesPerformer.StartPerform(TestCoroutine());
        }

        IEnumerator TestCoroutine()
        {
            Debug.Log("Start");
            yield return new WaitForSeconds(1f);
            Debug.Log("Continue");
        }
    }
}
