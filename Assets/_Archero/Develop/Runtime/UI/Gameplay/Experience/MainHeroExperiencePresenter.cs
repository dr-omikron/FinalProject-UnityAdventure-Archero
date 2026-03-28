using System;
using System.Collections.Generic;
using _Archero.Develop.Runtime.Configs.Gameplay;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.Features.MainHero;
using _Archero.Develop.Runtime.UI.CommonViews;
using _Archero.Develop.Runtime.UI.Core;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.UI.Gameplay.Experience
{
    public class MainHeroExperiencePresenter : IPresenter
    {
        private BarWithText _view;
        private MainHeroHolderService _heroHolderService;
        private ExperienceForUpgradeLevelConfig _levelUpConfig;
        private ReactiveVariable<float> _experience;
        private ReactiveVariable<int> _currentLevel;
        
        private List<IDisposable> _disposables = new List<IDisposable>();

        public MainHeroExperiencePresenter(
            MainHeroHolderService heroHolderService, 
            BarWithText view, 
            ExperienceForUpgradeLevelConfig levelUpConfig)
        {
            _heroHolderService = heroHolderService;
            _view = view;
            _levelUpConfig = levelUpConfig;
        }

        public void Initialize()
        {
            _disposables.Add(_heroHolderService.HeroRegister.Subscribe(OnMainHeroRegistered));
        }

        private void OnMainHeroRegistered(Entity hero)
        {
            _experience = hero.Experience;
            _currentLevel = hero.Level;

            _disposables.Add(_experience.Subscribe(OnCurrentExperienceChanged));
            _disposables.Add(_currentLevel.Subscribe(OnLevelChanged));
        }

        public void Dispose()
        {
            foreach (IDisposable disposable in _disposables)
                disposable.Dispose();
        }

        private void UpdateCurrentExperience(float value)
            => _view.UpdateSlider(value / _levelUpConfig.GetExperienceFor(_currentLevel.Value));

        private void UpdateBarText(int level) => _view.UpdateText($"Lv.{level}");

        private void OnLevelChanged(int arg1, int arg2) => UpdateBarText(_currentLevel.Value);

        private void OnCurrentExperienceChanged(float arg1, float arg2) => UpdateCurrentExperience(_experience.Value);

    }
}
