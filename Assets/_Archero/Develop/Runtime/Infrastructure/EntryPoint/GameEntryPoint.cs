using System.Collections;
using _Archero.Develop.Runtime.Infrastructure.DI;
using _Archero.Develop.Runtime.Utilities.ConfigsManagement;
using _Archero.Develop.Runtime.Utilities.CoroutinesManagement;
using UnityEngine;

namespace _Archero.Develop.Runtime.Infrastructure.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Старт проекта, сетап настроек");
            SetupAppSettings();

            Debug.Log("Процесс регистрации всего проекта");

            DIContainer container = new DIContainer();
            EntryPointRegistrations.Process(container);

            container.Resolve<ICoroutinesPerformer>().StartPerform(Initialize(container));
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        private IEnumerator Initialize(DIContainer container)
        {
            Debug.Log("Открывается загрузочный экран");
            Debug.Log("Начинается инициализация сервисов");

            yield return container.Resolve<ConfigsProviderService>().LoadAsync();
            yield return new WaitForSeconds(1.0f);

            Debug.Log("Завершается инициализация сервисов");
            Debug.Log("Закрывается загрузочный экран");
            
            Debug.Log("Начинается переход на другую сцену");
        }
    }
}
