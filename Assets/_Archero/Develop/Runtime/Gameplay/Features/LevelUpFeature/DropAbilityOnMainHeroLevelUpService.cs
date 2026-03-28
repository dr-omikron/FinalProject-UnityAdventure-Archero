
using System;
using System.Collections;
using System.Collections.Generic;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.Features.MainHero;
using _Archero.Develop.Runtime.Gameplay.Features.PauseFeature;
using _Archero.Develop.Runtime.Infrastructure.DI;
using _Archero.Develop.Runtime.UI.Gameplay;
using _Archero.Develop.Runtime.UI.Gameplay.AbilitySelectPopup;
using _Archero.Develop.Runtime.Utilities.CoroutinesManagement;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.LevelUpFeature
{
    public class DropAbilityOnMainHeroLevelUpService : IInitializable, IDisposable
    {
        private readonly MainHeroHolderService _mainHeroHolderService;
        private readonly GameplayPopupService _popupService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly IPauseService _pauseService;

        private readonly Queue<int> _levelUpRequests = new Queue<int>();

        private AbilitySelectPopupPresenter _popup;
        private Coroutine _selectAbilityProcess;

        private IDisposable _heroRegisteredDisposable;
        private IDisposable _heroLevelChangedDisposable;

        public DropAbilityOnMainHeroLevelUpService(
            MainHeroHolderService mainHeroHolderService, 
            GameplayPopupService popupService, 
            ICoroutinesPerformer coroutinesPerformer, IPauseService pauseService)
        {
            _mainHeroHolderService = mainHeroHolderService;
            _popupService = popupService;
            _coroutinesPerformer = coroutinesPerformer;
            _pauseService = pauseService;
        }

        private bool PopupIsOpened => _popup != null;

        public void Initialize()
        {
            _heroRegisteredDisposable = _mainHeroHolderService.HeroRegister.Subscribe(OnMainHeroRegistered);
        }

        public void Dispose()
        {
            _heroRegisteredDisposable.Dispose();
            _heroLevelChangedDisposable.Dispose();
        }

        private void OnMainHeroRegistered(Entity hero)
        {
            _heroLevelChangedDisposable = hero.Level.Subscribe(OnHeroLevelChanged);
        }

        private void OnHeroLevelChanged(int arg1, int currentLevel)
        {
            _levelUpRequests.Enqueue(currentLevel);

            if(_selectAbilityProcess != null)
                return;

            _selectAbilityProcess = _coroutinesPerformer.StartPerform(SelectAbilityProcess());
        }

        private IEnumerator SelectAbilityProcess()
        {
            while (_levelUpRequests.Count > 0)
            {
                int level = _levelUpRequests.Dequeue();

                _pauseService.Pause();
                _popup = _popupService.OpenAbilitySelectPopup(_mainHeroHolderService.MainHero, level, () =>
                {
                    _pauseService.Unpause();
                    _popup = null;
                });

                yield return new WaitUntil(() => PopupIsOpened == false);
            }

            _selectAbilityProcess = null;
        }

    }
}
