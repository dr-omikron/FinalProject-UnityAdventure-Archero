using System.Collections;
using _Archero.Develop.Runtime.Gameplay.Infrastructure;
using _Archero.Develop.Runtime.Infrastructure;
using _Archero.Develop.Runtime.Infrastructure.DI;
using _Archero.Develop.Runtime.Meta.Features.Wallet;
using _Archero.Develop.Runtime.Utilities.CoroutinesManagement;
using _Archero.Develop.Runtime.Utilities.DataManagement.DataProviders;
using _Archero.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace _Archero.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private WalletService _walletService;
        private PlayerDataProvider _playerDataProvider;
        private ICoroutinesPerformer _coroutinesPerformer;

        public override void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;
            MainMenuContextRegistrations.Process(container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("MainMenu Scene Initialized");

            _walletService = _container.Resolve<WalletService>();
            _playerDataProvider = _container.Resolve<PlayerDataProvider>();
            _coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("MainMenu Scene Started");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
                ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
                coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, new GameplayInputArgs(1)));
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _walletService.Add(CurrencyType.Gold, 10);
                Debug.Log("Золота осталось " + _walletService.GetCurrency(CurrencyType.Gold).Value);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if(_walletService.Enough(CurrencyType.Gold, 10))
                    _walletService.Spend(CurrencyType.Gold, 10);

                Debug.Log("Золота осталось " + _walletService.GetCurrency(CurrencyType.Gold).Value);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _coroutinesPerformer.StartPerform(_playerDataProvider.Save());
                Debug.Log("Сохранение было вызвано");
            }

        }
    }
}
