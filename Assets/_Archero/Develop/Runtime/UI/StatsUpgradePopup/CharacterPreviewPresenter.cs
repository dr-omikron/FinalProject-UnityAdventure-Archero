using _Archero.Develop.Runtime.UI.Core;
using _Archero.Develop.Runtime.Utilities.CoroutinesManagement;
using _Archero.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine.SceneManagement;

namespace _Archero.Develop.Runtime.UI.StatsUpgradePopup
{
    public class CharacterPreviewPresenter : IPresenter
    {
        private readonly SceneLoaderService _sceneLoaderService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        public CharacterPreviewPresenter(SceneLoaderService sceneLoaderService, ICoroutinesPerformer coroutinesPerformer)
        {
            _sceneLoaderService = sceneLoaderService;
            _coroutinesPerformer = coroutinesPerformer;
        }

        public void Initialize()
        {
            _coroutinesPerformer.StartPerform(
                _sceneLoaderService.LoadAsync(
                Scenes.CharacterPreviewScene,
                LoadSceneMode.Additive));
        }

        public void Dispose()
        {
            _coroutinesPerformer.StartPerform(_sceneLoaderService.UnloadAsync(Scenes.CharacterPreviewScene));
        }
    }
}
