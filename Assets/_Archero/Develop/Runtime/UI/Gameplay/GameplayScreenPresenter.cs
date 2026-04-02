using System;
using System.Collections.Generic;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.Features.MainHero;
using _Archero.Develop.Runtime.Meta.Features.Wallet;
using _Archero.Develop.Runtime.UI.Core;
using _Archero.Develop.Runtime.UI.Gameplay.Experience;
using _Archero.Develop.Runtime.UI.Gameplay.HealthDisplay;
using _Archero.Develop.Runtime.UI.Gameplay.Stages;
using _Archero.Develop.Runtime.UI.Wallet;

namespace _Archero.Develop.Runtime.UI.Gameplay
{
    public class GameplayScreenPresenter : IPresenter
    {
        private readonly GameplayScreenView _screen;
        private readonly GameplayPresentersFactory _presentersFactory;
        private readonly ProjectPresenterFactory _projectPresenterFactory;
        private readonly MainHeroHolderService _heroHolderService;
        private readonly List<IPresenter> _childPresenters = new List<IPresenter>();
        private EntityHealthDisplayPresenter _entityHealthDisplayPresenter;
        
        private IDisposable _mainHeroHolderServiceDisposable;
        private CurrencyPresenter _mainHeroCoinsPresenter;

        public GameplayScreenPresenter(
            GameplayScreenView screen, 
            GameplayPresentersFactory presentersFactory, 
            MainHeroHolderService heroHolderService, 
            ProjectPresenterFactory projectPresenterFactory)
        {
            _screen = screen;
            _presentersFactory = presentersFactory;
            _heroHolderService = heroHolderService;
            _projectPresenterFactory = projectPresenterFactory;
        }

        public void Initialize()
        {
            CreateStageNumber();
            CreateEntityHealthDisplay();
            CreateMainHeroExperienceView();

            _mainHeroHolderServiceDisposable = _heroHolderService.HeroRegister.Subscribe(OnHeroRegistered);

            foreach (var childPresenter in _childPresenters)
                childPresenter.Initialize();
        }

        private void OnHeroRegistered(Entity hero)
        {
            _mainHeroCoinsPresenter =
                _projectPresenterFactory.CreateCurrencyPresenter(_screen.CoinsView, hero.Coins, CurrencyType.Gold);

            _mainHeroCoinsPresenter.Initialize();
        }

        public void Dispose()
        {
            _mainHeroHolderServiceDisposable?.Dispose();
            _mainHeroCoinsPresenter.Dispose();

            foreach (var childPresenter in _childPresenters)
                childPresenter.Dispose();

            _childPresenters.Clear();
        }

        public void LateUpdate()
        {
            _entityHealthDisplayPresenter.LateUpdate();
        }

        private void CreateStageNumber()
        {
            StagePresenter stagePresenter = _presentersFactory.CreateStagePresenter(_screen.StageNumberView);
            _childPresenters.Add(stagePresenter);
        }

        private void CreateEntityHealthDisplay()
        {
            _entityHealthDisplayPresenter =
                _presentersFactory.CreateEntityHealthDisplayPresenter(_screen.EntitiesHealthDisplay);

            _childPresenters.Add(_entityHealthDisplayPresenter);
        }

        private void CreateMainHeroExperienceView()
        {
            MainHeroExperiencePresenter mainHeroExperiencePresenter = _presentersFactory.CreateMainHeroExperiencePresenter(_screen.ExperienceBarView);
            _childPresenters.Add(mainHeroExperiencePresenter);
        }
    }
}
